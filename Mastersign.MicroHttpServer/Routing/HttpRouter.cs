using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal sealed class HttpRouter : IHttpRequestHandler
    {
        private readonly IList<HttpRoute> _routes = new List<HttpRoute>();

        public void When(IHttpRouteCondition condition, IHttpRequestHandler handler)
        {
            _routes.Add(new HttpRoute(condition, handler));
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            foreach (var route in _routes)
            {
                var matchingResult = route.Condition.Match(context);
                if (matchingResult.IsMatch)
                {
                    var subContext = context.Dive(matchingResult);
                    return route.Handler.Handle(subContext, next);
                }
            }

            // Route not found, call next and thereby reverse dive
            return next();
        }
    }
}
