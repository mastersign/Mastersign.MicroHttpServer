using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public sealed class EmptyHttpResponse : HttpResponseBase
    {
        public EmptyHttpResponse(HttpResponseCode code, IStringLookup headers) 
            : base(code, headers) 
        { }

        public static EmptyHttpResponse Create(
            HttpResponseCode code = HttpResponseCode.OK,
            bool keepAlive = true)
        {
            return new EmptyHttpResponse(code, new ListStringLookup(new[] {
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Content-Type", "text/html"),
                new KeyValuePair<string, string>("Connection", keepAlive ? "Keep-Alive" : "Close"),
                new KeyValuePair<string, string>("Content-Length", "0"),
            }));
        }

        public override Task WriteBody(Stream _)
        {
            return Task.Factory.GetCompleted();
        }
    }
}