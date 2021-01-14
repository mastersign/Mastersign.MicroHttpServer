using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class NotFoundHandler : IHttpRequestHandler
    {
        public Task Handle(IHttpContext context, Func<Task> _) => context.RespondNotFound();
    }
}
