using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Mastersign.MicroHttpServer.Test")]

namespace Mastersign.MicroHttpServer
{
    public class HttpMethodProvider : IHttpMethodProvider
    {
        public static readonly IHttpMethodProvider Default = new HttpMethodProviderCache(new HttpMethodProvider());

        internal HttpMethodProvider() { }

        public HttpMethod Provide(string name)
        {
            var capitalName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            return (HttpMethod)Enum.Parse(typeof(HttpMethod), capitalName);
        }
    }
}