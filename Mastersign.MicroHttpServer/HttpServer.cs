using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public sealed class HttpServer : IDisposable
    {
        private readonly IList<IHttpRequestHandler> _handlers = new List<IHttpRequestHandler>();
        private readonly IList<IHttpListener> _listeners = new List<IHttpListener>();
        private readonly IList<ILogger> _loggers = new List<ILogger>();
        private readonly ILogger _logger;

        private readonly IHttpRequestProvider _requestProvider;

        public int BufferSize { get; set; } = 1024 * 8;
        public int PostStreamLimit { get; set; } = 1024 * 1024;

        private bool _isActive;

        public HttpServer(IHttpRequestProvider requestProvider)
        {
            _logger = new AggregateLogger(_loggers);
            _requestProvider = requestProvider;
        }

        public HttpServer() : this(new HttpRequestProvider())
        { }

        public void Dispose()
        {
            _isActive = false;
        }

        public void Use(IHttpRequestHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Use(IHttpListener listener)
        {
            _listeners.Add(listener);
        }

        public void Use(ILogger log)
        {
            _loggers.Add(log);
        }

        public void Start()
        {
            _isActive = true;

            foreach (var listener in _listeners)
            {
                var tempListener = listener;

                Task.Factory.StartNew(() => Listen(tempListener));
            }

            _logger.Info("Embedded Micro HTTP Server started.");
        }

        private async void Listen(IHttpListener listener)
        {
            var aggregatedHandler = _handlers.Aggregate();

            while (_isActive)
            {
                try
                {
                    new HttpClientHandler(
                        await listener.GetClient().ConfigureAwait(false),
                        aggregatedHandler,
                        _requestProvider,
                        _logger,
                        BufferSize,
                        PostStreamLimit);
                }
                catch (Exception e)
                {
                    _logger.Warn($"Error while getting client", e);
                }
            }

            _logger.Info("Embedded Micro HTTP Server stopped.");
        }
    }
}