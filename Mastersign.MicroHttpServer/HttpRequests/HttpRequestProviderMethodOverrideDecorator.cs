using System.IO;
using System.Net;
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

        public ILogger Logger
        {
            get => _child.Logger;
            set => _child.Logger = value;
        }

        public async Task<IHttpRequest> Provide(Stream stream, EndPoint remoteEndPoint)
        {
            var childValue = await _child.Provide(stream, remoteEndPoint).ConfigureAwait(false);

            return childValue != null
                ? childValue.Headers.TryGetByName("X-HTTP-Method-Override", out var methodName)
                    ? new HttpRequestMethodDecorator(childValue, HttpMethodProvider.Default.Provide(methodName))
                    : childValue
                : null;
        }
    }
}