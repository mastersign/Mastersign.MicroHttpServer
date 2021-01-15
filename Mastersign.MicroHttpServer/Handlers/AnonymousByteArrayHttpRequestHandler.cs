using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class AnonymousByteArrayHttpRequestHandler : IHttpRequestHandler
    {
        private readonly Func<IHttpContext, byte[]> _method;
        private readonly string _contentType;

        public AnonymousByteArrayHttpRequestHandler(Func<IHttpContext, byte[]> method, string contentType = "application/octet-stream")
        {
            _method = method;
            _contentType = contentType;
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            var result = _method(context);
            if (result != null)
            {
                context.Response = ByteArrayHttpResponse.Create(result, 0, result.Length, contentType: _contentType);
                return Task.Factory.GetCompleted();
            }

            return next();
        }
    }

}
