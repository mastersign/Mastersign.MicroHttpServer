using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class ExceptionHandler : IHttpRequestHandler
    {
        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            try
            {
                await next().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await context.RespondInternalServerError(e.ToString(), keepAlive: false).ConfigureAwait(false);
            }
        }
    }
}
