using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Mastersign.MicroHttpServer
{
    internal class AsynchronousLogDispatcher : ILogger
    {
        private readonly IList<ILogger> _loggers;

        private readonly BlockingCollection<LogEvent> _events;

        private readonly Thread _workerThread;

        public LogLevel MinLevel { get; set; }

        public AsynchronousLogDispatcher(IList<ILogger> loggers, int bufferSize = 1000, LogLevel minLevel = LogLevel.Verbose)
        {
            _loggers = loggers;
            _events = new BlockingCollection<LogEvent>(new ConcurrentQueue<LogEvent>(), bufferSize);

            MinLevel = minLevel;

            _workerThread = new Thread(Work);
            _workerThread.Name = typeof(AsynchronousLogDispatcher).FullName;
            _workerThread.Start();
        }

        public void Dispose()
        {
            _events.CompleteAdding();
            _workerThread.Join();
        }

        private void Work()
        {
            foreach (var logEvent in _events.GetConsumingEnumerable())
            {
                foreach (var logger in _loggers) logger.Log(logEvent);
            }
        }

        public void Log(LogEvent logEvent)
        {
            if ((int)logEvent.Level < (int)MinLevel) return;
            try
            {
                _events.Add(logEvent);
            }
            catch (InvalidOperationException)
            {
                // does not accept more events
            }
        }
    }
}
