using System.Collections.Generic;
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

        IReadOnlyList<string> Route { get; }

        dynamic State { get; }

        IHttpContext Dive(int segments = 1);
    }
}