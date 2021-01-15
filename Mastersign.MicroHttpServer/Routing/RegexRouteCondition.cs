using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Regex Condition: {HttpMethod}, {Pattern,nq}")]
    public class RegexRouteCondition : IHttpRouteCondition
    {
        private readonly HttpMethod? _method;
        private readonly string _pattern;
        private readonly Regex _regex;

        internal HttpMethod? HttpMethod => _method;
        internal string Pattern => _pattern;

        public RegexRouteCondition(HttpMethod? method, string regex, bool rightOpen = false, bool ignoreCase = false)
        {
            _method = method;
            _pattern = regex;

            regex = regex.Trim();
            // force the regex to start at the beginning
            if (!regex.StartsWith("^")) regex = "^" + regex;

            if (rightOpen) regex.TrimEnd('$');

            if (rightOpen && !regex.EndsWith("/") && !regex.EndsWith("(/|$)"))
            {
                regex += "(/|$)";
            }

            // force the regex to anchor at the end
            if (!rightOpen && !regex.EndsWith("$")) regex += "$";

            var options = RegexOptions.Compiled
                | RegexOptions.ExplicitCapture
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace;
            if (ignoreCase) options |= RegexOptions.IgnoreCase;
            _regex = new Regex(regex, options);
        }

        public static RegexRouteCondition FromRoutePattern(HttpMethod? method, string pattern, bool rightOpen = false, bool ignoreCase = false)
            => new RegexRouteCondition(method, RegexPatternFromRoutePattern(pattern), rightOpen, ignoreCase);

        private static readonly Regex _escapedOpenCurlyPattern = new Regex(@"\\\\\\\{");
        private static readonly Regex _escapedPlaceholderPattern = new Regex(@"\\\{(?<name>.+?)\}");

        internal static string RegexPatternFromRoutePattern(string pattern)
        {
            pattern = pattern.TrimStart('/');
            pattern = Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".");
            pattern = _escapedOpenCurlyPattern.Replace(pattern, "{");
            pattern = _escapedPlaceholderPattern.Replace(pattern, m => $"(?<{m.Groups["name"].Value}>[^/])");
            pattern = pattern.Replace("{", "\\{");
            return pattern;
        }

        public HttpRouteMatchResult Match(IHttpContext context)
        {
            var request = context.Request;
            if (_method.HasValue && request.Method != _method.Value)
            {
                return HttpRouteMatchResult.NoMatch;
            }
            var match = _regex.Match(context.Route);
            context.Logger.Trace($"Regex Condition /{_regex}/ : {context.Route} => {match.Success}");
            if (match.Success)
            {
                return new HttpRouteMatchResult(
                    trimmedRoute: context.Route.Substring(match.Length),
                    routeParameters: _regex.GetGroupNames()
                        .Select(key => new KeyValuePair<string, string>(key, match.Groups[key].Value)));
            }
            else
            {
                return HttpRouteMatchResult.NoMatch;
            }
        }
    }
}
