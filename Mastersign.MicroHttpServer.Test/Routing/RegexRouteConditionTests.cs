using System;
using Xunit;

namespace Mastersign.MicroHttpServer.Test
{
    public class RegexRouteConditionTests
    {
        [Fact]
        public void RegexPatternFromRoutePatternTest()
        {
            var routePattern = "/abc\\{--}/_(TEST)_/{p1}/*/{p2}/test-?.TXT";
            var regexPattern = "/abc\\{--}/_\\(TEST\\)_/(?<p1>[^/])/.*/(?<p2>[^/])/test-.\\.TXT";

            Assert.Equal(regexPattern, RegexRouteCondition.RegexPatternFromRoutePattern(routePattern));
        }
    }
}
