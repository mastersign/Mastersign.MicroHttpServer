using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class HttpRequestProvider : IHttpRequestProvider
    {
        private static readonly Stream _emptyStream = new EmptyStream();

        public async Task<IHttpRequest> Provide(Stream stream)
        {
            // parse the http request

            // use UTF-8 decoding, dispite the fact that only ASCII characters are allowed in header fields,
            // some clients send UTF-8 sequences anyway
            var streamReader = new StreamReader(stream, Encoding.UTF8);
            var request = await streamReader.ReadLineAsync().ConfigureAwait(false);

            if (request == null) return null;

            var firstSpace = request.IndexOf(' ');
            var lastSpace = request.LastIndexOf(' ');

            var tokens = new[] {
                request.Substring(0, firstSpace),
                request.Substring(firstSpace + 1, lastSpace - firstSpace - 1),
                request.Substring(lastSpace + 1)
            };

            if (tokens.Length != 3) return null;

            var httpMethod = HttpMethodProvider.Default.Provide(tokens[0]);
            var httpProtocol = tokens[2];

            var uri = new Uri("http://server" + tokens[1], UriKind.Absolute);

            // get the headers
            var headersRaw = new List<KeyValuePair<string, string>>();
            string line;
            while (!string.IsNullOrEmpty(line = await streamReader.ReadLineAsync().ConfigureAwait(false)))
            {
                var currentLine = line;

                var headerKvp = SplitHeader(currentLine);
                headersRaw.Add(headerKvp);
            }
            var headers = headersRaw.ToStringLookup();

            var contentStream = headers.TryGetByName("Content-Length", out long length)
                ? new LimitedStream(stream, length, length)
                : _emptyStream;

            return new HttpRequest(httpMethod, uri, httpProtocol, headers, contentStream);
        }

        private KeyValuePair<string, string> SplitHeader(string header)
        {
            var index = header.IndexOf(": ", StringComparison.Ordinal);
            return new KeyValuePair<string, string>(header.Substring(0, index), header.Substring(index + 2));
        }
    }
}