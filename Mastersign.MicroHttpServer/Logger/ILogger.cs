using System;

namespace Mastersign.MicroHttpServer
{
    public interface ILogger
    {
        void Log(LogLevel level, string message, Exception e = null);
    }
}