using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal sealed class HttpClientHandler
    {
        private const int DEFAULT_REQUEST_BUFFER_SIZE = 1024 * 8;
        private const int DEFAULT_REQUEST_STREAM_LIMIT = 1024 * 1024;
        private const int DEFAULT_RESPONSE_STREAM_LIMIT = -1;
        private const int RESPONSE_HEADER_BUFFER_SIZE = 1024;

        private readonly ILogger _logger;
        private readonly EndPoint _remoteEndPoint;
        private readonly Func<IHttpContext, Task> _requestHandler;
        private readonly IHttpRequestProvider _requestProvider;
        private readonly Stream _stream;
        private readonly int _readLimit;
        private readonly int _writeLimit;

        public IClient Client { get; }

        public DateTime LastOperationTime { get; private set; }

        public HttpClientHandler(
            IClient client,
            Func<IHttpContext, Task> requestHandler,
            IHttpRequestProvider requestProvider,
            ILogger logger = null,
            int requestBufferSize = DEFAULT_REQUEST_BUFFER_SIZE,
            int requestStreamLimit = DEFAULT_REQUEST_STREAM_LIMIT,
            int responseStreamLimit = DEFAULT_RESPONSE_STREAM_LIMIT)
        {
            Client = client;
            _remoteEndPoint = client.RemoteEndPoint;
            _requestHandler = requestHandler;
            _requestProvider = requestProvider;
            _logger = logger;
            _readLimit = requestStreamLimit;
            _writeLimit = responseStreamLimit;

            _stream = new BufferedStream(Client.Stream, requestBufferSize);

            _logger.Debug($"{_remoteEndPoint} connect");

            Task.Factory.StartNew(Process);

            UpdateLastOperationTime();
        }

        private async void Process()
        {
            try
            {
                while (Client.Connected)
                {
                    var limitedStream = new NotFlushingStream(new LimitedStream(_stream, _readLimit, _writeLimit));

                    var request = await _requestProvider.Provide(limitedStream).ConfigureAwait(false);

                    if (request != null)
                    {
                        UpdateLastOperationTime();

                        var context = new HttpContext(_logger, Client.RemoteEndPoint, request);
                        var url = request.Url;

                        _logger.Debug($"{Client.RemoteEndPoint} : {request.Method.ToString().ToUpperInvariant()} {url.PathAndQuery}");

                        await _requestHandler(context).ConfigureAwait(false);

                        if (context.Response != null)
                        {
                            await WriteResponse(context, limitedStream).ConfigureAwait(false);
                            await limitedStream.ExplicitFlushAsync().ConfigureAwait(false);

                            if (!request.KeepAliveConnection() || context.Response.CloseConnection) Client.Close();
                        }

                        UpdateLastOperationTime();
                    }
                    else
                    {
                        Client.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Warn($"{_remoteEndPoint} error while serving", e);
                Client.Close();
            }

            _logger.Debug($"{_remoteEndPoint} disconnect");
        }

        private async Task WriteResponse(HttpContext context, Stream stream)
        {
            var response = context.Response;

            using (var writer = new StreamWriter(stream, Encoding.ASCII, RESPONSE_HEADER_BUFFER_SIZE, leaveOpen: true))
            {
                // Headers
                var httpLine = string.Format("HTTP/1.1 {0} {1}", (int)response.ResponseCode, response.ResponseCode);
                await writer.WriteLineAsync(httpLine).ConfigureAwait(false);

                foreach (var header in response.Headers)
                {
                    var headerLine = string.Format("{0}: {1}", header.Key, header.Value);
                    await writer.WriteLineAsync(headerLine).ConfigureAwait(false);
                }

                // Cookies
                if (context.Cookies.Touched)
                {
                    await writer.WriteAsync(context.Cookies.ToCookieData()).ConfigureAwait(false);
                }

                // Empty line
                await writer.WriteLineAsync().ConfigureAwait(false);

                // Flush header writer
                await writer.FlushAsync().ConfigureAwait(false);
            }

            // Body
            await response.WriteBody(stream).ConfigureAwait(false);
        }

        public void ForceClose()
        {
            Client.Close();
        }

        private void UpdateLastOperationTime()
        {
            LastOperationTime = DateTime.Now;
        }
    }
}