using System;
using System.Collections.Concurrent;

namespace Mastersign.MicroHttpServer
{
    public class HttpMethodProviderCache : IHttpMethodProvider
    {
        private readonly ConcurrentDictionary<string, HttpMethod> _cache = new ConcurrentDictionary<string, HttpMethod>();

        private readonly Func<string, HttpMethod> _childProvide;

        public HttpMethodProviderCache(IHttpMethodProvider child)
        {
            _childProvide = child.Provide;
        }

        public HttpMethod Provide(string name)
        {
            return _cache.GetOrAdd(name, _childProvide);
        }
    }
}