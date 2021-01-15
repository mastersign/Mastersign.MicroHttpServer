using System;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpServer : IHttpRoutable, IDisposable
    {
        IHttpServer Use(IHttpListener listener);

        IHttpServer Use(ILogger log);
        
        void Start();
    }
}
