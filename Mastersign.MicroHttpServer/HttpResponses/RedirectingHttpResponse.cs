using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Redirecting Response: {ResponseCode} -> {Location,nq}")]
    public sealed class RedirectingHttpResponse : HttpResponseBase
    {
        internal string Location => Headers.GetByName("Location");

        public RedirectingHttpResponse(HttpResponseCode code, IStringLookup headers) 
            : base(code, headers) 
        {
            if ((int)code < 300 && (int)code > 399)
            {
                throw new ArgumentException("The response code must be a redirection code 3xx.", nameof(code));
            }
        }

        public static RedirectingHttpResponse Create(
            string location,
            HttpResponseCode code = HttpResponseCode.TemporaryRedirect,
            bool keepAlive = true)
        {
            return new RedirectingHttpResponse(code, new ListStringLookup(new[] {
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Content-Type", "text/html"),
                new KeyValuePair<string, string>("Connection", keepAlive ? "Keep-Alive" : "Close"),
                new KeyValuePair<string, string>("Content-Length", "0"),
                new KeyValuePair<string, string>("Location", location),
            }));
        }

        public override Task WriteBody(Stream _)
        {
            return Task.Factory.GetCompleted();
        }
    }
}