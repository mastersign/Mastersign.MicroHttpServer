using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    /// <summary>
    ///     An <see cref="IHttpRequestHandler" />
    ///     That lets the following <see cref="IHttpRequestHandler" />s in the chain to run
    ///     and afterwards tries to compress the returned response by the "Accept-Encoding" header that
    ///     given from the client.
    ///     The compressors given in the constructor are prefered by the order that they are given.
    /// </summary>
    public class CompressionMiddelware : IHttpRequestHandler
    {
        private static readonly char[] Seperator = { ',' };
        private readonly ICompressor[] _compressors;

        /// <summary>
        ///     Creates an instance of <see cref="CompressionMiddelware" />
        /// </summary>
        /// <param name="compressors">The compressors to use, Ordered by preference</param>
        public CompressionMiddelware(params ICompressor[] compressors)
        {
            _compressors = compressors;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            await next().ConfigureAwait(false);

            if (context.Response == null) return;

            if (!context.Request.Headers.TryGetByName("Accept-Encoding", out var encodingNames)) return;

            var encodings = encodingNames.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);

            var compressor = _compressors
                .FirstOrDefault(c => encodings.Contains(c.Name, StringComparer.InvariantCultureIgnoreCase));

            if (compressor == null) return;

            context.Response = await compressor.Compress(context.Response).ConfigureAwait(false);
        }
    }
}