using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpResponse
    {
        /// <summary>
        ///     Gets the status line of this http response,
        ///     The first line that will be sent to the client.
        /// </summary>
        HttpResponseCode ResponseCode { get; }

        IStringLookup Headers { get; }

        bool CloseConnection { get; }

        Task WriteBody(Stream stream);
    }
}