using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Async Byte Array Generator Handler: {ContentType}")]
    public class AsyncAnonymousByteArrayHttpRequestHandler : IHttpRequestHandler
    {
        private readonly Func<IHttpContext, Task<byte[]>> _method;
        private readonly string _contentType;

        internal string ContentType => _contentType;

        public AsyncAnonymousByteArrayHttpRequestHandler(Func<IHttpContext, Task<byte[]>> method, string contentType = "application/octet-stream")
        {
            _method = method;
            _contentType = contentType;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            var result = await _method(context);
            if (result != null)
            {
                context.Response = ByteArrayHttpResponse.Create(result, 0, result.Length, contentType: _contentType);
                return;
            }
            await next();
        }
    }

}
