using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Byte Array Response: {ResponseCode}, {ContentLength} Bytes, {ContentType}")]
    public sealed class ByteArrayHttpResponse : HttpResponseBase
    {
        private readonly byte[] _body;
        private readonly int _offset;
        private readonly int _length;

        internal int ContentLength => Headers.GetByName<int>("Content-Length");
        internal string ContentType => Headers.GetByName("Content-Type");

        public ByteArrayHttpResponse(byte[] body, int offset, int length, HttpResponseCode code, IStringLookup headers)
            : base(code, headers)
        {
            _body = body;
            _offset = offset;
            _length = length;
        }

        public ByteArrayHttpResponse(byte[] body, HttpResponseCode code, IStringLookup headers)
            : base(code, headers)
        {
            _body = body;
            _offset = 0;
            _length = body.Length;
        }

        public static ByteArrayHttpResponse Create(
            byte[] body, int offset, int length,
            HttpResponseCode code = HttpResponseCode.OK,
            string contentType = "application/octet-stream",
            bool keepAlive = true)
        {
            return new ByteArrayHttpResponse(body, offset, length, code, new ListStringLookup(new[] {
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Connection", keepAlive ? "Keep-Alive" : "Close"),
                new KeyValuePair<string, string>("Content-Type", contentType),
                new KeyValuePair<string, string>("Content-Length", length.ToString(CultureInfo.InvariantCulture))
            }));
        }

        public override async Task WriteBody(Stream stream)
        {
            await stream.WriteAsync(_body, _offset, _length);
        }
    }
}
