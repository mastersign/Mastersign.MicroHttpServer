using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("HttpApp: Name = {Name}")]
    public sealed class HttpApp : IHttpRoutable, IHttpRequestHandler
    {
        private readonly HttpRoutingPipeline _pipeline = new HttpRoutingPipeline();
        private readonly IHttpRoutable _parent;

        public string Name { get; }

        public HttpApp(string name = "app") 
        {
            _parent = null;
            Name = name;
        }

        internal HttpApp(IHttpRoutable parent, string name)
        {
            _parent = parent;
            Name = name;
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
            return _parent;
        }

        public IHttpRoutable Merge()
        {
            if (_parent == null) throw new InvalidOperationException("This routing branch has no parent.");
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
