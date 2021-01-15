using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{DebugLabel}")]

    public struct HttpRouteMatchResult
    {
        public static readonly HttpRouteMatchResult NoMatch;

        public static readonly HttpRouteMatchResult Match = new HttpRouteMatchResult(null, null);

        internal string DebugLabel => IsMatch
            ? TrimmedRoute != null
                ? RouteParameters != null
                    ? $"Route match with {RouteParameters.Count()} route params and trimmed route: {TrimmedRoute}"
                    : $"Route match with trimmed route: {TrimmedRoute}"
                : RouteParameters != null
                    ? $"Route match with {RouteParameters.Count()} route params"
                    : $"Route match without context modification"
            : "No route match";

        public bool IsMatch { get; }

        public string TrimmedRoute { get; }

        public IEnumerable<KeyValuePair<string, string>> RouteParameters { get; }

        public HttpRouteMatchResult(
            string trimmedRoute = null,
            IEnumerable<KeyValuePair<string, string>> routeParameters = null)
        {
            IsMatch = true;
            TrimmedRoute = trimmedRoute;
            RouteParameters = routeParameters;
        }

        public bool ContainsContextModification => IsMatch 
            && TrimmedRoute != null 
            && RouteParameters != null;

        public override bool Equals(object obj)
        {
            return obj is HttpRouteMatchResult result &&
                   IsMatch == result.IsMatch &&
                   TrimmedRoute == result.TrimmedRoute &&
                   EqualityComparer<IEnumerable<KeyValuePair<string, string>>>.Default.Equals(RouteParameters, result.RouteParameters);
        }

        public override int GetHashCode()
        {
            var hashCode = 977042922;
            hashCode = hashCode * -1521134295 + IsMatch.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TrimmedRoute);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<KeyValuePair<string, string>>>.Default.GetHashCode(RouteParameters);
            return hashCode;
        }

        public static bool operator ==(HttpRouteMatchResult left, HttpRouteMatchResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HttpRouteMatchResult left, HttpRouteMatchResult right)
        {
            return !(left == right);
        }
    }
}
