using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpRequestProvider
    {
        /// <summary>
        ///     Provides an <see cref="IHttpRequest" /> based on the context of the stream,
        ///     May return null / throw exceptions on invalid requests.
        /// </summary>
        Task<IHttpRequest> Provide(Stream stream);
    }
}