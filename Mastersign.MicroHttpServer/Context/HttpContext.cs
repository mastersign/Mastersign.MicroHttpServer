using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace Mastersign.MicroHttpServer
{
    internal class HttpContext : IHttpContext
    {
        private readonly ExpandoObject _state = new ExpandoObject();

        public HttpContext(ILogger logger, IHttpRequest request, EndPoint remoteEndPoint)
        {
            Logger = logger;
            Request = request;
            Route = request.PathSegments;
            RemoteEndPoint = remoteEndPoint;
            Cookies = new CookiesStorage(Request.Headers.GetByNameOrDefault("Cookie", string.Empty));
        }

        public ILogger Logger { get; }

        public IHttpRequest Request { get; }

        public IHttpResponse Response { get; set; }

        public ICookiesStorage Cookies { get; }

        public EndPoint RemoteEndPoint { get; }

        public IReadOnlyList<string> Route { get; }

        public dynamic State => _state;

        public IHttpContext Dive(int segments) => new HttpSubContext(this, Route.Skip(segments));
    }
}