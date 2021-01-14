using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpRequestHandler
    {
        Task Handle(IHttpContext context, Func<Task> next);
    }
}