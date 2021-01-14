using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class HttpServerExtensions
    {
        public static void ListenTo(this HttpServer server, IPEndPoint endpoint, X509Certificate serverCertificate = null)
            => server.Use(serverCertificate != null
                ? new ListenerSslDecorator(new TcpListenerAdapter(new TcpListener(endpoint)), serverCertificate)
                : (IHttpListener)new TcpListenerAdapter(new TcpListener(endpoint)));

        public static void ListenTo(this HttpServer server, IPAddress address, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(new IPEndPoint(address, port), serverCertificate);

        public static void ListenTo(this HttpServer server, string ipString, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Parse(ipString), port, serverCertificate);

        public static void ListenTo(this HttpServer server, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Any, port, serverCertificate);

        public static void ListenToLoopback(this HttpServer server, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Loopback, port, serverCertificate);

        public static void LogToConsole(this HttpServer server, LogLevel minLevel = LogLevel.Warning, bool withColor = true)
            => server.Use(new ConsoleLogger(minLevel, withColor));

        public static void Use(this HttpServer server,
            Func<IHttpContext, IHttpResponse> method)
            => server.Use((ctx, _) =>
            {
                ctx.Response = method(ctx);
                return Task.Factory.GetCompleted();
            });

        public static void Use(this HttpServer server, Func<IHttpContext, Func<Task>, Task> method)
            => server.Use(new AnonymousHttpRequestHandler(method));

        public static void Use(this HttpServer server, Func<IHttpContext, string> method, string contentType = "text/html; charset=utf-8")
            => server.Use(new AnonymousStringHttpRequestHandler(method, contentType));

        public static PathSegmentRouter PathRouter(this HttpServer server)
        {
            var router = new PathSegmentRouter();
            server.Use(router);
            return router;
        }
    }
}
