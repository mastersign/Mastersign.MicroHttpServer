using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Regex Condition: {Pattern,nq}")]
    public class RegexRouteCondition : IHttpRouteCondition
    {
        private readonly string _pattern;
        private readonly Regex _regex;

        internal string Pattern => _pattern;

        public RegexRouteCondition(string regex, bool rightOpen = false, bool ignoreCase = false)
        {
            _pattern = regex;

            regex = regex.Trim();
            // force the regex to start at the beginning
            if (!regex.StartsWith("^")) regex = "^" + regex;

            regex.TrimEnd('$');

            if (rightOpen && !regex.EndsWith("/") && !regex.EndsWith("(/|$)"))
            {
                regex += "(/|$)";
            }

            // force the regex to anchor at the end
            if (!rightOpen)
            {
                if (!regex.EndsWith("/") && !regex.EndsWith("/?")) regex += "/?";
                regex += "$";
            }

            var options = RegexOptions.Compiled
                | RegexOptions.ExplicitCapture
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace;
            if (ignoreCase) options |= RegexOptions.IgnoreCase;
            _regex = new Regex(regex, options);
        }

        public static RegexRouteCondition FromRoutePattern(string pattern, bool rightOpen = false, bool ignoreCase = false)
            => new RegexRouteCondition(RegexPatternFromRoutePattern(pattern), rightOpen, ignoreCase);

        private static readonly Regex _escapedOpenCurlyPattern = new Regex(@"\\\\\\\{");
        private static readonly Regex _escapedPlaceholderPattern = new Regex(@"\\\{(?<name>.+?)\}");

        internal static string RegexPatternFromRoutePattern(string pattern)
        {
            pattern = pattern.Trim('/');
            pattern = Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".");
            pattern = _escapedOpenCurlyPattern.Replace(pattern, "{");
            pattern = _escapedPlaceholderPattern.Replace(pattern, m => $"(?<{m.Groups["name"].Value}>[^/]+)");
            pattern = pattern.Replace("{", "\\{");
            return pattern;
        }

        public HttpRouteMatchResult Match(IHttpContext context)
        {
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
