using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class HttpRequestExtensions
    {
        public static bool KeepAliveConnection(this IHttpRequest request)
            => request.Headers.TryGetByName("Connection", out var value)
                && value.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase);

        public static long ContentLength(this IHttpRequest request)
            => request.Headers.TryGetByName("Content-Length", out long length) ? length : 0L;

        public static string ContentType(this IHttpRequest request)
            => request.Headers.TryGetByName("Content-Type", out string contentType) ? contentType : null;

        public static byte[] ReadContentAsBytes(this IHttpRequest request)
        {
            if (request.ContentStream == null) return null;
            using (var bufferStream = new MemoryStream())
            {
                request.ContentStream.CopyTo(bufferStream);
                return bufferStream.ToArray();
            }
        }

        public static async Task<byte[]> ReadContentAsBytesAsync(this IHttpRequest request)
        {
            if (request.ContentStream == null) return null;
            using (var bufferStream = new MemoryStream())
            {
                await request.ContentStream.CopyToAsync(bufferStream);
                return bufferStream.ToArray();
            }
        }

        public static string ReadContentAsString(this IHttpRequest request, Encoding encoding = null)
        {
            if (request.ContentStream == null) return null;
            using (var r = new StreamReader(request.ContentStream, encoding ?? Encoding.UTF8))
            {
                return r.ReadToEnd();
            }
        }

        public static async Task<string> ReadContentAsStringAsync(this IHttpRequest request, Encoding encoding = null)
        {
            if (request.ContentStream == null) return null;
            using (var r = new StreamReader(request.ContentStream, encoding ?? Encoding.UTF8))
            {
                return await r.ReadToEndAsync();
            }
        }
    }
}
