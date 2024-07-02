using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class ListenerSslDecorator : IHttpListener
    {
        private readonly X509Certificate _certificate;
        private readonly IHttpListener _child;
        private readonly SslProtocols _sslProtocols;

        public ListenerSslDecorator(
            IHttpListener child,
            X509Certificate certificate,
            SslProtocols sslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12
        )
        {
            _child = child;
            _certificate = certificate;
            _sslProtocols = sslProtocols;
        }

        public async Task<IClient> GetClient()
        {
            return new ClientSslDecorator(await _child.GetClient().ConfigureAwait(false), _certificate, _sslProtocols);
        }

        public override string ToString()
        {
            return $"{_child} (SSL)";
        }
    }
}