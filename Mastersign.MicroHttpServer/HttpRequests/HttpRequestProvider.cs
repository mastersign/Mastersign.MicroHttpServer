using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class HttpRequestProvider : IHttpRequestProvider
    {
        public int TotalHeaderLimit { get; set; } = 1024 * 4;

        public int RequestLineLimit { get; set; } = 1024;

        public int HeaderLineLimit { get; set; } = 1024;

        public bool AllowUTF8Header { get; set; }

        public ILogger Logger { get; set; }

        public async Task<IHttpRequest> Provide(Stream stream, EndPoint remoteEndPoint)
        {
            var streamReader = new StreamReader(stream, AllowUTF8Header ? Encoding.UTF8 : Encoding.ASCII);
            var consumed = 0;

            ParsedRequestLine requestLine = ParsedRequestLine.Failed;
            IStringLookup headers = null;

            try
            {
                await Task.Run(() =>
                {
                    requestLine = ParseRequestLine(streamReader, ref consumed);
                });
                if (!requestLine.Success) return null;
                await Task.Run(() =>
                {
                    headers = ParseHeaderLines(streamReader, ref consumed);
                });
                if (headers == null) return null;
            }
            catch (IOException e)
            {
                if (e.InnerException is Win32Exception w32Exc && (uint)w32Exc.NativeErrorCode == 0x80090327)
                {
                    // failed to decode data from SSL/TLS stream
                    Logger?.Debug($"{remoteEndPoint} decoding stream failed: " + w32Exc.Message);
                }
                else
                {
                    throw;
                }
            }

            Stream contentStream = null;
            try
            {
                var contentLength = long.Parse(headers.GetByName("Content-Length"));
                if (contentLength > 0)
                {
                    contentStream = new ContentStream(stream, contentLength);
                }
            }
            catch (Exception) 
            {
                // invalid or missing content length header
                // leads to null content stream
            }
            return new HttpRequest(
                requestLine.Method, requestLine.Uri, requestLine.Protocol,
                headers, contentStream);
        }

        private ParsedRequestLine ParseRequestLine(StreamReader r, ref int consumed)
        {
            var request = ReadLimitedLine(r, RequestLineLimit, ref consumed);

            if (request == null) return ParsedRequestLine.Failed;

            var firstSpace = request.IndexOf(' ');
            var lastSpace = request.LastIndexOf(' ');

            if (lastSpace <= firstSpace) return ParsedRequestLine.Failed;

            var tokens = new[] {
                request.Substring(0, firstSpace),
                request.Substring(firstSpace + 1, lastSpace - firstSpace - 1),
                request.Substring(lastSpace + 1)
            };

            var method = HttpMethodProvider.Default.Provide(tokens[0]);
            var uriString = tokens[1];
            var uri = uriString.StartsWith("http://") || uriString.StartsWith("https://")
                ? new Uri(uriString, UriKind.Absolute)
                : new Uri("http://0.0.0.0" + uriString, UriKind.Absolute);
            var protocol = tokens[2];

            return new ParsedRequestLine(method, uri, protocol);
        }

        private IStringLookup ParseHeaderLines(StreamReader r, ref int consumed)
        {
            var headersRaw = new List<KeyValuePair<string, string>>();
            string line;
            while (!string.IsNullOrEmpty(line = ReadLimitedLine(r, HeaderLineLimit, ref consumed)))
            {
                if (consumed > TotalHeaderLimit)
                {
                    return null;
                }
                var headerKvp = SplitHeader(line);
                if (headerKvp.HasValue) headersRaw.Add(headerKvp.Value);
            }
            if (line == null) return null;
            return headersRaw.ToStringLookup();
        }

        private string ReadLimitedLine(StreamReader r, int limit, ref int consumed)
        {
            var sb = new StringBuilder();
            var exceededLimit = false;
            int c = 0;
            var hadCR = false;
            while (c >= 0)
            {
                try
                {
                    c = r.Read();
                }
                catch (IOException ioex) when (ioex.InnerException is SocketException soex)
                {
                    Logger?.Trace($"{soex.ErrorCode} {soex.SocketErrorCode}");
                    if (soex.SocketErrorCode == SocketError.TimedOut)
                        continue;
                    else
                        return null;
                }
                consumed ++;
                if (sb.Length > limit)
                {
                    exceededLimit = true;
                    break;
                }
                if (c == '\r')
                {
                    hadCR = true;
                }
                else if (hadCR && c == '\n')
                {
                    break;
                }
                else
                {
                    hadCR = false;
                    sb.Append((char)c);
                }
            }
            return exceededLimit ? null : sb.ToString();
        }

        private static KeyValuePair<string, string>? SplitHeader(string header)
        {
            var index = header.IndexOf(": ", StringComparison.Ordinal);
            return index > 0
                ? new KeyValuePair<string, string>(header.Substring(0, index), header.Substring(index + 2))
                : (KeyValuePair<string, string>?)null;
        }

        private struct ParsedRequestLine
        {
            public static ParsedRequestLine Failed = new ParsedRequestLine { Success = false };

            public bool Success;
            public HttpMethod Method;
            public Uri Uri;
            public string Protocol;

            public ParsedRequestLine(HttpMethod method, Uri uri, string protocol)
            {
                Success = true;
                Method = method;
                Uri = uri;
                Protocol = protocol;
            }
        }
    }
}