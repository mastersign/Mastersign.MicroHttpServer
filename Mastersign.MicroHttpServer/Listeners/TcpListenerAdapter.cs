using System.Net.Sockets;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class TcpListenerAdapter : IHttpListener
    {
        private readonly TcpListener _listener;

        public bool NoDelay { get; set; }

        public TcpListenerAdapter(TcpListener listener, bool noDelay = false)
        {
            NoDelay = noDelay;
            _listener = listener;
            _listener.Start();
        }

        public async Task<IClient> GetClient()
        {
            var client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
            client.NoDelay = NoDelay;
            return new TcpClientAdapter(client);
        }

        public override string ToString()
        {
            return $"{_listener.LocalEndpoint}";
        }
    }
}