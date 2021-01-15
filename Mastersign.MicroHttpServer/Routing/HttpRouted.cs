using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal sealed class HttpRouted : IHttpRoutable, IHttpRequestHandler
    {
        private readonly HttpRoutingPipeline _pipeline = new HttpRoutingPipeline();
        private readonly IHttpRoutable _parent;

        public HttpRouted(IHttpRoutable parent)
        {
            _parent = parent;
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
            if (fallback != null)
            {
                _pipeline.PushUnconditional(fallback);
            }
            return _parent;
        }

        private Func<IHttpContext, Task> _pipelineHandler = null;

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            if (_pipelineHandler == null) _pipelineHandler = _pipeline.GetPipelineHandler();

            await _pipelineHandler(context).ConfigureAwait(false);

            if (context.Response == null)
            {
                await next().ConfigureAwait(false);
            }
        }
    }
}
