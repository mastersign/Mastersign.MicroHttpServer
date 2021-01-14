using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class UrlEncodedContentMiddleware : IHttpRequestHandler
    {
        private const int DEFAULT_BUFFER_SIZE = 1024;

        private int BufferSize { get; set; }
        private bool IgnoreContentType { get; set; }

        public UrlEncodedContentMiddleware(
            bool ignoreContentType = false,
            int bufferSize = DEFAULT_BUFFER_SIZE)
        {
            IgnoreContentType = ignoreContentType;
            BufferSize = bufferSize;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            if (IgnoreContentType ||
                context.Request.ContentType() == "application/x-www-form-urlencoded")
            {
                using (var r = new StreamReader(context.Request.ContentStream, Encoding.UTF8, false, BufferSize, true))
                {
                    var text = await r.ReadToEndAsync().ConfigureAwait(false);
                    context.Request.Content = new QueryStringLookup(text);
                }
            }
            await next().ConfigureAwait(false);
        }
    }
}
