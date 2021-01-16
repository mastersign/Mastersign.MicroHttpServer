using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Const Byte Array Handler: {Response.ResponseCode}, {Content.Length} Bytes, {ContentType}")]
    public class ConstByteArrayHttpRequestHandler : IHttpRequestHandler
    {
        public byte[] Content { get; }
        
        public string ContentType { get; }

        public IHttpResponse Response { get; }

        public ConstByteArrayHttpRequestHandler(byte[] content,
            HttpResponseCode code = HttpResponseCode.OK,
            string contentType = "application/octet-stream", 
            bool keepALive = true)
        {
            Content = content;
            ContentType = contentType;
            Response = ByteArrayHttpResponse.Create(content, 0, content.Length,
                contentType: contentType,
                code: code,
                keepAlive: keepALive);
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            context.Response = Response;
            return Task.Factory.GetCompleted();
        }
    }
}
