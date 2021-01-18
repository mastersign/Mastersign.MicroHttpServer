using System;

namespace Mastersign.MicroHttpServer
{
    public class LogEvent
    {
        public DateTime Timestamp { get; }

        public LogLevel Level { get; }

        public string Message { get; }

        public Exception Exception { get; }

        public LogEvent(DateTime timestamp, LogLevel level, string message, Exception exception = null)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
            Exception = exception;
        }
    }
}
