using System;
using System.Collections.Generic;

namespace Mastersign.MicroHttpServer
{
    internal class AggregateLogger : ILogger
    {
        private readonly IList<ILogger> _loggers;

        public AggregateLogger(IList<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Log(LogLevel level, string message, Exception e = null)
        {
            foreach (var logger in _loggers) logger.Log(level, message, e);
        }
    }
}
