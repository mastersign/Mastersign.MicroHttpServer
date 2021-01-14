using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class GZipCompressor : ICompressor
    {
        public static readonly ICompressor Default = new GZipCompressor();

        public string Name => "gzip";

        public Task<IHttpResponse> Compress(IHttpResponse response) 
            => CompressedResponse.CreateGZip(response);
    }
}