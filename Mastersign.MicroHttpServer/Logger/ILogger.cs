using System;

namespace Mastersign.MicroHttpServer
{
    public interface ILogger : IDisposable
    {
        LogLevel MinLevel { get; set; }

        void Log(LogEvent logEvent);
    }
}