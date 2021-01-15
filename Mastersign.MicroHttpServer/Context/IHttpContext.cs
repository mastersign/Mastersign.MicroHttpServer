using System.Net;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpContext
    {
        ILogger Logger { get; }

        IHttpRequest Request { get; }

        IHttpResponse Response { get; set; }

        ICookiesStorage Cookies { get; }

        EndPoint RemoteEndPoint { get; }

        string Route { get; }

        IStringLookup RouteParameters { get; }

        dynamic State { get; }

        IHttpContext Dive(HttpRouteMatchResult matchResult);
    }
}
