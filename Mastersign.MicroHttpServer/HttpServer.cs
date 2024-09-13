using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Mastersign.MicroHttpServer.Test")]

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("HttpServer: Name = {Name}")]
    public sealed class HttpServer : IDisposable, IHttpServer
    {
        private readonly IList<IHttpListener> _listeners = new List<IHttpListener>();
        private readonly IList<ILogger> _loggers = new List<ILogger>();

        private readonly ILogger _logger;
        private readonly IHttpRequestProvider _requestProvider;

        private readonly HttpRoutingPipeline _pipeline = new HttpRoutingPipeline();

        public string Name { get; }

        public int BufferSize { get; set; } = 0; // 1024 * 8;
        public int RequestStreamLimit { get; set; } = 1024 * 1024;
        public int ResponseStreamLimit { get; set; } = -1;

        private bool _isActive;

        public HttpServer(
            string name = "server",
            IHttpRequestProvider requestProvider = null,
            LogLevel minLogLevel = LogLevel.Verbose, 
            int logBuffer = 1000)
        {
            Name = name;
            _logger = new AsynchronousLogDispatcher(_loggers, 
                minLevel: minLogLevel,
                bufferSize: logBuffer);
            _requestProvider = requestProvider ?? new HttpRequestProvider();
            _requestProvider.Logger = _logger;
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

        public IHttpRoutable Branch(IHttpRouteCondition condition, string name = "branch")
        {
            var routed = new HttpApp(this, name);
            _pipeline.PushConditional(condition, routed, () => new HttpRouter());
            return routed;
        }

        public IHttpRoutable EndWith(IHttpRequestHandler fallback)
        {
            _pipeline.PushUnconditional(fallback);
            return null;
        }

        public IHttpRoutable Merge()
        {
            throw new InvalidOperationException("The server has no parent.");
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

            _logger.Info("Embedded Micro HTTP Server started.");

            foreach (var listener in _listeners)
            {
                var tempListener = listener;
                Task.Factory.StartNew(() => Listen(tempListener));
                _logger.Info("Listening to: " + listener);
            }
        }

        private async void Listen(IHttpListener listener)
        {
            var pipelineHandler = _pipeline.GetPipelineHandler();

            while (_isActive)
            {
                try
                {
                    var client = await listener.GetClient().ConfigureAwait(false);

                    new HttpClientHandler(client, pipelineHandler,
                        _requestProvider,
                        _logger,
                        BufferSize,
                        RequestStreamLimit,
                        ResponseStreamLimit);
                }
                catch (Exception e)
                {
                    _logger.Debug($"Error while getting client", e);
                }
            }

            _logger.Info("Embedded Micro HTTP Server stopped.");
        }
    }
}