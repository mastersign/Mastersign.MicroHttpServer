using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Anonymous Handler")]
    public class AnonymousHttpRequestHandler : IHttpRequestHandler
    {
        private readonly Func<IHttpContext, Func<Task>, Task> _method;

        public AnonymousHttpRequestHandler(Func<IHttpContext, Func<Task>, Task> method)
        {
            _method = method;
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            return _method(context, next);
        }
    }

}
