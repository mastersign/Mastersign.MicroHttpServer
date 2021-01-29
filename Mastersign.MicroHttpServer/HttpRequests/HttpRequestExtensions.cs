using System;

namespace Mastersign.MicroHttpServer
{
    public static class HttpRequestExtensions
    {
        public static long ContentLength(this IHttpRequest request)
            => request.Headers.TryGetByName("Content-Length", out long length) ? length : 0L;

        public static string ContentType(this IHttpRequest request)
            => request.Headers.TryGetByName("Content-Type", out string contentType) ? contentType : null;

        public static bool KeepAliveConnection(this IHttpRequest request)
            => request.Headers.TryGetByName("Connection", out var value)
                && value.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase);
    }
}
