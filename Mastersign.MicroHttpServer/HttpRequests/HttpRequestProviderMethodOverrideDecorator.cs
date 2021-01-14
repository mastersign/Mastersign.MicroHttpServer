using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class HttpRequestProviderMethodOverrideDecorator : IHttpRequestProvider
    {
        private readonly IHttpRequestProvider _child;

        public HttpRequestProviderMethodOverrideDecorator(IHttpRequestProvider child)
        {
            _child = child;
        }

        public async Task<IHttpRequest> Provide(Stream stream)
        {
            var childValue = await _child.Provide(stream).ConfigureAwait(false);

            return childValue != null
                ? childValue.Headers.TryGetByName("X-HTTP-Method-Override", out var methodName)
                    ? new HttpRequestMethodDecorator(childValue, HttpMethodProvider.Default.Provide(methodName))
                    : childValue
                : null;
        }
    }
}