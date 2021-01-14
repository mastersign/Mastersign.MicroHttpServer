using System.IO;
using System.Net;

namespace Mastersign.MicroHttpServer
{
    public interface IClient
    {
        Stream Stream { get; }

        bool Connected { get; }

        EndPoint RemoteEndPoint { get; }

        void Close();
    }
}