using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Mastersign.MicroHttpServer
{
    internal class HttpSubContext : IHttpContext
    {
        private readonly IHttpContext _parent;

        public HttpSubContext(IHttpContext parent, IEnumerable<string> routeSegments)
        {
            _parent = parent;
            Route = routeSegments.ToList();
        }

        public IReadOnlyList<string> Route { get; }

        public ILogger Logger => _parent.Logger;

        public IHttpRequest Request => _parent.Request;

        public IHttpResponse Response { 
            get => _parent.Response;
            set => _parent.Response = value;
        }

        public ICookiesStorage Cookies => _parent.Cookies;

        public EndPoint RemoteEndPoint => _parent.RemoteEndPoint;

        public dynamic State => _parent.State;

        public IHttpContext Dive(int segments = 1) => new HttpSubContext(this, Route.Skip(segments));
    }
}
