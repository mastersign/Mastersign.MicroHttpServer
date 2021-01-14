using System.Net.Sockets;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class TcpListenerAdapter : IHttpListener
    {
        private readonly TcpListener _listener;

        public TcpListenerAdapter(TcpListener listener)
        {
            _listener = listener;
            _listener.Start();
        }

        public async Task<IClient> GetClient()
        {
            return new TcpClientAdapter(await _listener.AcceptTcpClientAsync().ConfigureAwait(false));
        }
    }
}