using System.Collections.Generic;
using System.Linq;

namespace Mastersign.MicroHttpServer
{
    public class CombinedRouteCondition : IHttpRouteCondition
    {
        private readonly IHttpRouteCondition[] _conditions;

        public CombinedRouteCondition(params IHttpRouteCondition[] conditions)
        {
            var cs = new List<IHttpRouteCondition>();
            foreach (var c in conditions)
            {
                if (c is CombinedRouteCondition cc)
                    cs.AddRange(cc._conditions);
                else
                    cs.Add(c);
            }
            _conditions = cs.ToArray();
        }

        public HttpRouteMatchResult Match(IHttpContext context)
        {
            var match = HttpRouteMatchResult.Match;
            foreach (var condition in _conditions)
            {
                var m = condition.Match(context);
                if (m.IsMatch)
                {
                    var trimmedRoute = match.TrimmedRoute;
                    if (m.TrimmedRoute != null &&
                        (trimmedRoute == null || m.TrimmedRoute.Length < trimmedRoute.Length))
                    {
                        trimmedRoute = m.TrimmedRoute;
                    }
                    var routeParameters = match.RouteParameters;
                    if (routeParameters != null && m.RouteParameters != null)
                    {
                        routeParameters = routeParameters.Concat(m.RouteParameters);
                    }
                    else
                    {
                        routeParameters = m.RouteParameters;
                    }
                    match = new HttpRouteMatchResult(trimmedRoute, routeParameters);
                }
                else
                {
                    return HttpRouteMatchResult.NoMatch;
                }
            }
            return match;
        }
    }
}
