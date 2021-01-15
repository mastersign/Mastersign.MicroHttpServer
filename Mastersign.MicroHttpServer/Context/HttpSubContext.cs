using System.Net;

namespace Mastersign.MicroHttpServer
{
    internal class HttpSubContext : IHttpContext
    {
        private readonly IHttpContext _parent;

        public HttpSubContext(IHttpContext parent, HttpRouteMatchResult match)
        {
            _parent = parent;
            if (match.TrimmedRoute != null)
            {
                _route = match.TrimmedRoute.TrimStart('/');
            }
            if (match.RouteParameters != null)
            {
                _routeParameters = match.RouteParameters.ToChainedStringLookup(parent.RouteParameters);
            }
        }

        private readonly string _route;
        public string Route => _route ?? _parent.Route;

        private readonly IStringLookup _routeParameters;
        public IStringLookup RouteParameters => _routeParameters ?? _parent.RouteParameters;

        public ILogger Logger => _parent.Logger;

        public IHttpRequest Request => _parent.Request;

        public IHttpResponse Response { 
            get => _parent.Response;
            set => _parent.Response = value;
        }

        public ICookiesStorage Cookies => _parent.Cookies;

        public EndPoint RemoteEndPoint => _parent.RemoteEndPoint;

        public dynamic State => _parent.State;

        public IHttpContext Dive(HttpRouteMatchResult match) => new HttpSubContext(this, match);
    }
}
