using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace Mastersign.MicroHttpServer
{
    internal class HttpContext : IHttpContext
    {
        private readonly ExpandoObject _state = new ExpandoObject();

        public HttpContext(ILogger logger, EndPoint remoteEndPoint, IHttpRequest request)
        {
            Logger = logger;
            RemoteEndPoint = remoteEndPoint;
            Request = request;
            Route = request.Url.AbsolutePath.TrimStart('/');
            RouteParameters = EmptyStringLookup.Instance;
            Cookies = new CookiesStorage(Request.Headers.GetByNameOrDefault("Cookie", string.Empty));
        }

        public ILogger Logger { get; }

        public IHttpRequest Request { get; }

        public IHttpResponse Response { get; set; }

        public ICookiesStorage Cookies { get; }

        public EndPoint RemoteEndPoint { get; }

        public string Route { get; }

        public IStringLookup RouteParameters { get; }

        public dynamic State => _state;

        public IHttpContext Dive(HttpRouteMatchResult match) => new HttpSubContext(this, match);
    }
}