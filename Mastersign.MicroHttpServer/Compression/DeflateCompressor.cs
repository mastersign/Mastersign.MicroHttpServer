using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class DeflateCompressor : ICompressor
    {
        public static readonly ICompressor Default = new DeflateCompressor();

        public string Name => "deflate";

        public Task<IHttpResponse> Compress(IHttpResponse response) 
            => CompressedResponse.CreateDeflate(response);
    }
}