using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class ListenerSslDecorator : IHttpListener
    {
        private readonly X509Certificate _certificate;
        private readonly IHttpListener _child;

        public ListenerSslDecorator(IHttpListener child, X509Certificate certificate)
        {
            _child = child;
            _certificate = certificate;
        }

        public async Task<IClient> GetClient()
        {
            return new ClientSslDecorator(await _child.GetClient().ConfigureAwait(false), _certificate);
        }

        public override string ToString()
        {
            return $"{_child} (SSL)";
        }
    }
}