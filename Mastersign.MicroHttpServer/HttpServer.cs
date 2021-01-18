using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Mastersign.MicroHttpServer.Test")]

namespace Mastersign.MicroHttpServer
{
    public sealed class HttpServer : IDisposable, IHttpServer
    {
        private readonly IList<IHttpListener> _listeners = new List<IHttpListener>();
        private readonly IList<ILogger> _loggers = new List<ILogger>();

        private readonly ILogger _logger;
        private readonly IHttpRequestProvider _requestProvider;

        private readonly HttpRoutingPipeline _pipeline = new HttpRoutingPipeline();

        public int BufferSize { get; set; } = 1024 * 8;
        public int PostStreamLimit { get; set; } = 1024 * 1024;

        private bool _isActive;

        public HttpServer(IHttpRequestProvider requestProvider = null, LogLevel minLogLevel = LogLevel.Verbose, int logBuffer = 1000)
        {
            _logger = new AsynchronousLogDispatcher(_loggers, 
                minLevel: minLogLevel,
                bufferSize: logBuffer);
            _requestProvider = requestProvider ?? new HttpRequestProvider();
        }

        public void Dispose()
        {
            _isActive = false;
            _logger.Dispose();
        }

        public IHttpRoutable Use(IHttpRequestHandler handler)
        {
            _pipeline.PushUnconditional(handler);
            return this;
        }

        public IHttpRoutable UseWhen(IHttpRouteCondition condition, IHttpRequestHandler handler)
        {
            _pipeline.PushConditional(condition, handler, () => new HttpRouter());
            return this;
        }

        public IHttpRoutable Dive(IHttpRouteCondition condition)
        {
            var routed = new HttpRouted(this);
            _pipeline.PushConditional(condition, routed, () => new HttpRouter());
            return routed;
        }

        public IHttpRoutable Ascent(IHttpRequestHandler fallback = null)
        {
            throw new NotSupportedException("You can not ascent from the routing context of the server");
        }

        public IHttpServer Use(IHttpListener listener)
        {
            _listeners.Add(listener);
            return this;
        }

        public IHttpServer Use(ILogger log)
        {
            _loggers.Add(log);
            return this;
        }

        public void Start()
        {
            _isActive = true;

            foreach (var listener in _listeners)
            {
                var tempListener = listener;

                Task.Factory.StartNew(() => Listen(tempListener));
            }

            _logger.Info("Embedded Micro HTTP Server started.");
        }

        private async void Listen(IHttpListener listener)
        {
            var pipelineHandler = _pipeline.GetPipelineHandler();

            while (_isActive)
            {
                try
                {
                    new HttpClientHandler(
                        await listener.GetClient().ConfigureAwait(false),
                        pipelineHandler,
                        _requestProvider,
                        _logger,
                        BufferSize,
                        PostStreamLimit);
                }
                catch (Exception e)
                {
                    _logger.Warn($"Error while getting client", e);
                }
            }

            _logger.Info("Embedded Micro HTTP Server stopped.");
        }
    }
}