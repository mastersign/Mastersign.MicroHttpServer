using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Async String Generator Handler: {ContentType}")]
    public class AsyncAnonymousStringHttpRequestHandler : IHttpRequestHandler
    {
        private readonly Func<IHttpContext, Task<string>> _method;
        private readonly string _contentType;

        internal string ContentType => _contentType;

        public AsyncAnonymousStringHttpRequestHandler(Func<IHttpContext, Task<string>> method, string contentType = "text/html; charset=utf-8")
        {
            _method = method;
            _contentType = contentType;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            var result = await _method(context);
            if (result != null)
            {
                context.Response = StringHttpResponse.Create(result, contentType: _contentType);
                return;
            }
            await next();
        }
    }

}
