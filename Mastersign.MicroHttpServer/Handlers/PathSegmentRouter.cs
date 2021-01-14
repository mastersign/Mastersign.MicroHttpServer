using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class PathSegmentRouter : IHttpRequestHandler
    {
        private IHttpRequestHandler _fallbackHandler;
        private readonly IDictionary<string, IHttpRequestHandler> _handlers;

        public PathSegmentRouter(StringComparer patternComparer = null)
        {
            _handlers = new Dictionary<string, IHttpRequestHandler>(
                patternComparer ?? StringComparer.InvariantCultureIgnoreCase);
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            var currentSegment = string.Empty;

            if (context.Route.Count > 0)
            {
                currentSegment = context.Route[0];
            }

            if (_handlers.TryGetValue(currentSegment, out var value))
            {
                var subContext = context.Dive();
                return value.Handle(subContext, next);
            }

            if (_fallbackHandler != null)
            {
                return _fallbackHandler.Handle(context, next);
            }

            // Route not found, Call next.
            return next();
        }

        public PathSegmentRouter With(string segment, IHttpRequestHandler handler)
        {
            _handlers.Add(segment.Trim('/'), handler);

            return this;
        }

        public PathSegmentRouter Without(IHttpRequestHandler handler)
        {
            _fallbackHandler = handler;
            return this;
        }
    }
}