using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<IHttpRequest> Provide(Stream stream)
        {
            var streamReader = new StreamReader(stream, AllowUTF8Header ? Encoding.UTF8 : Encoding.ASCII);
            var consumed = 0;

            ParsedRequestLine requestLine = ParsedRequestLine.Failed;
            IStringLookup headers = null;

            await Task.Run(() =>
            {
                requestLine = ParseRequestLine(streamReader, ref consumed);
                if (!requestLine.Success) return;

                headers = ParseHeaderLines(streamReader, ref consumed);
            });
            if (!requestLine.Success || headers == null) return null;

            IHttpRequest request = new HttpRequest(requestLine.Method, requestLine.Uri, requestLine.Protocol, headers, stream);
            return request;
        }

        private ParsedRequestLine ParseRequestLine(StreamReader r, ref int consumed)
        {
            var request = ReadLimitedLine(r, RequestLineLimit);

            if (request == null) return ParsedRequestLine.Failed;
            consumed += request.Length + 2;

            var firstSpace = request.IndexOf(' ');
            var lastSpace = request.LastIndexOf(' ');

            if (lastSpace <= firstSpace) return ParsedRequestLine.Failed;

            var tokens = new[] {
                request.Substring(0, firstSpace),
                request.Substring(firstSpace + 1, lastSpace - firstSpace - 1),
                request.Substring(lastSpace + 1)
            };
            if (tokens.Length != 3) return ParsedRequestLine.Failed;

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
            while (!string.IsNullOrEmpty(line = ReadLimitedLine(r, HeaderLineLimit)))
            {
                consumed += line.Length + 2;
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

        private static string ReadLimitedLine(StreamReader r, int limit)
        {
            var sb = new StringBuilder();
            var exceededLimit = false;
            int c;
            var hadCR = false;
            while ((c = r.Read()) >= 0)
            {
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