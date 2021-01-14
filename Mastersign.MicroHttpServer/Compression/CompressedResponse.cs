using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class CompressedResponse : IHttpResponse
    {
        private readonly MemoryStream _memoryStream;

        public CompressedResponse(IHttpResponse child, MemoryStream memoryStream, string encoding)
        {
            _memoryStream = memoryStream;

            ResponseCode = child.ResponseCode;
            CloseConnection = child.CloseConnection;
            Headers = new ListStringLookup(child.Headers
                .Where(h => !h.Key.Equals("Content-Length", StringComparison.InvariantCultureIgnoreCase))
                .Concat(new[] {
                    new KeyValuePair<string, string>("Content-Length", memoryStream.Length.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("Content-Encoding", encoding)
                })
                .ToList());
        }

        public async Task WriteBody(Stream stream)
        {
            _memoryStream.Position = 0;
            await _memoryStream.CopyToAsync(stream).ConfigureAwait(false);
        }

        public HttpResponseCode ResponseCode { get; }

        public IStringLookup Headers { get; }

        public bool CloseConnection { get; }

        public static async Task<IHttpResponse> Create(string name, IHttpResponse child, Func<Stream, Stream> streamFactory)
        {
            var memoryStream = new MemoryStream();
            using (var deflateStream = streamFactory(memoryStream))
            {
                await child.WriteBody(deflateStream).ConfigureAwait(false);
            }
            return new CompressedResponse(child, memoryStream, name);
        }

        public static Task<IHttpResponse> CreateDeflate(IHttpResponse child) 
            => Create("deflate", child, 
                s => new DeflateStream(s, CompressionMode.Compress, leaveOpen: true));

        public static Task<IHttpResponse> CreateGZip(IHttpResponse child) 
            => Create("gzip", child, 
                s => new GZipStream(s, CompressionMode.Compress, leaveOpen: true));
    }
}