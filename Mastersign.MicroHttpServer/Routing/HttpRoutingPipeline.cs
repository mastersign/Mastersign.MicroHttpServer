using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Routing Pipeline with {HandlerCount} handlers")]

    internal sealed class HttpRoutingPipeline
    {
        private readonly IList<IHttpRequestHandler> _handlers = new List<IHttpRequestHandler>();

        private HttpRouter _currentRouter = null;

        internal int HandlerCount => _handlers.Count;

        public void PushUnconditional(IHttpRequestHandler handler)
        {
            _currentRouter = null;
            _handlers.Add(handler);
        }

        public void PushConditional(IHttpRouteCondition condition, IHttpRequestHandler handler, Func<HttpRouter> routerFactory)
        {
            if (_currentRouter == null)
            {
                _currentRouter = routerFactory();
                _handlers.Add(_currentRouter);
            }
            _currentRouter.When(condition, handler);
        }

        public Func<IHttpContext, Task> GetPipelineHandler() => _handlers.Aggregate();
    }
}
