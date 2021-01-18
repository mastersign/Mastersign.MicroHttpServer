using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Mastersign.MicroHttpServer
{
    public static class HttpServerExtensions
    {
        public static IHttpServer ListenTo(this IHttpServer server, IPEndPoint endpoint, X509Certificate serverCertificate = null)
            => server.Use(serverCertificate != null
                ? new ListenerSslDecorator(new TcpListenerAdapter(new TcpListener(endpoint)), serverCertificate)
                : (IHttpListener)new TcpListenerAdapter(new TcpListener(endpoint)));

        public static IHttpServer ListenTo(this IHttpServer server, IPAddress address, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(new IPEndPoint(address, port), serverCertificate);

        public static IHttpServer ListenTo(this IHttpServer server, string ipString, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Parse(ipString), port, serverCertificate);

        public static IHttpServer ListenTo(this IHttpServer server, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Any, port, serverCertificate);

        public static IHttpServer ListenToLoopback(this IHttpServer server, int port = 8080, X509Certificate serverCertificate = null)
            => server.ListenTo(IPAddress.Loopback, port, serverCertificate);

        public static IHttpServer LogToConsole(this IHttpServer server,
            LogLevel minLevel = LogLevel.Warning,
            string timestampFormat = "yyyy-MM-dd HH:mm:ss",
            bool withColor = true)
            => server.Use(new ConsoleLogger(minLevel, timestampFormat, withColor));

    }
}
