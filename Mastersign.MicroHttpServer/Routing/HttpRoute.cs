using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Route: {Condition} -> {Handler}")]
    internal struct HttpRoute
    {
        public IHttpRouteCondition Condition { get; }

        public IHttpRequestHandler Handler { get; }

        public HttpRoute(IHttpRouteCondition condition, IHttpRequestHandler handler)
        {
            Condition = condition;
            Handler = handler;
        }
    }
}
