using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public sealed class StreamHttpResponse : HttpResponseBase
    {
        private readonly Stream _body;

        public StreamHttpResponse(Stream body, HttpResponseCode code, IStringLookup headers)
            : base(code, headers)
        {
            _body = body;
        }

        public static StreamHttpResponse Create(
            Stream body,
            HttpResponseCode code = HttpResponseCode.OK,
            string contentType = "text/html; charset=utf-8",
            bool keepAlive = true)
        {
            return new StreamHttpResponse(body, code, new ListStringLookup(new[] {
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Connection", keepAlive ? "Keep-Alive" : "Close"),
                new KeyValuePair<string, string>("Content-Type", contentType),
                new KeyValuePair<string, string>("Content-Length", body.Length.ToString(CultureInfo.InvariantCulture))
            }));
        }

        public override async Task WriteBody(Stream stream)
        {
            await _body.CopyToAsync(stream).ConfigureAwait(false);
        }
    }
}