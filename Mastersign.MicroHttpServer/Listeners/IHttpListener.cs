using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpListener
    {
        Task<IClient> GetClient();
    }
}