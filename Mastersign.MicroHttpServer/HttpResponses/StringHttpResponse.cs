using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public sealed class StringHttpResponse : HttpResponseBase
    {
        private static readonly Encoding _encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        private const int DEFAULT_BUFFER_SIZE = 1024;
        private readonly string _body;

        public StringHttpResponse(string body, HttpResponseCode code, IStringLookup headers) 
            : base(code, headers)
        {
            _body = body;
        }

        public static StringHttpResponse Create(string body, string contentType,
            HttpResponseCode code = HttpResponseCode.OK,
            bool keepAlive = true)
        {
            return new StringHttpResponse(body, code, new ListStringLookup(new[] {
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Content-Type", contentType),
                new KeyValuePair<string, string>("Connection", keepAlive ? "Keep-Alive" : "Close"),
                new KeyValuePair<string, string>("Content-Length", Encoding.UTF8.GetByteCount(body).ToString(CultureInfo.InvariantCulture))
            }));
        }

        public static StringHttpResponse Html(string html,
            HttpResponseCode code = HttpResponseCode.OK,
            bool keepAlive = true)
            => Create(html, "text/html; charset=utf-8", code, keepAlive);

        public static StringHttpResponse Text(string html,
            HttpResponseCode code = HttpResponseCode.OK,
            bool keepAlive = true)
            => Create(html, "text/plain; charset=utf-8", code, keepAlive);

        public override async Task WriteBody(Stream stream)
        {
            using (var writer = new StreamWriter(stream, _encoding, DEFAULT_BUFFER_SIZE, true))
            {
                await writer.WriteAsync(_body).ConfigureAwait(false);
            }
        }
    }
}