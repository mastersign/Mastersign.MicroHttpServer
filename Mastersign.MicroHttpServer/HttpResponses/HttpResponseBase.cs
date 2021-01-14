using System;
using System.IO;
using System.Threading.Tasks;


namespace Mastersign.MicroHttpServer
{
    public abstract class HttpResponseBase : IHttpResponse
    {
        protected HttpResponseBase(HttpResponseCode code, IStringLookup headers)
        {
            ResponseCode = code;
            Headers = headers;
        }

        public abstract Task WriteBody(Stream stream);

        public HttpResponseCode ResponseCode { get; }

        public IStringLookup Headers { get; }

        public bool CloseConnection =>
            !Headers.TryGetByName("Connection", out var value) ||
            !value.Equals("Keep-Alive", StringComparison.InvariantCultureIgnoreCase);
    }
}