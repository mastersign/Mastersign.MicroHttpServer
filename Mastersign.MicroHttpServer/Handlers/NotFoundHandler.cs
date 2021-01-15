using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Not Found Handler")]
    public class NotFoundHandler : IHttpRequestHandler
    {
        public Task Handle(IHttpContext context, Func<Task> _) => context.RespondNotFound();
    }
}
