using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mastersign.MicroHttpServer
{
    public class ClientSslDecorator : IClient
    {
        private readonly IClient _child;
        private readonly SslStream _sslStream;

        public ClientSslDecorator(
            IClient child,
            X509Certificate certificate,
            SslProtocols protocols = SslProtocols.Tls11 | SslProtocols.Tls12)
        {
            _child = child;
            _sslStream = new SslStream(_child.Stream);
            _sslStream.AuthenticateAsServer(certificate,
                clientCertificateRequired: false,
                enabledSslProtocols: protocols,
                checkCertificateRevocation: true);
        }

        public Stream Stream => _sslStream;

        public bool Connected => _child.Connected;

        public void Close() => _child.Close();

        public EndPoint RemoteEndPoint => _child.RemoteEndPoint;
    }
}