using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class TimingMiddleware : IHttpRequestHandler
    {
        public LogLevel LogLevel { get; set; }

        public TimingMiddleware(LogLevel logLevel = LogLevel.Verbose)
        {
            LogLevel = logLevel;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next().ConfigureAwait(false);
            stopWatch.Stop();
            context.Logger.Log(LogLevel, 
                $"{context.RemoteEndPoint} : {context.Request.Url.PathAndQuery} ({stopWatch.ElapsedMilliseconds} ms)");
        }
    }
}
