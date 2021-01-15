using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Method Condition: {HttpMethod}")]
    public class HttpMethodCondition : IHttpRouteCondition
    {
        public static readonly HttpMethodCondition GetInstance = new HttpMethodCondition(HttpMethod.Get);
        public static readonly HttpMethodCondition PostInstance = new HttpMethodCondition(HttpMethod.Post);
        public static readonly HttpMethodCondition PutInstance = new HttpMethodCondition(HttpMethod.Put);
        public static readonly HttpMethodCondition DeleteInstance = new HttpMethodCondition(HttpMethod.Delete);
        public static readonly HttpMethodCondition OptionsInstance = new HttpMethodCondition(HttpMethod.Options);
        public static readonly HttpMethodCondition HeadInstance = new HttpMethodCondition(HttpMethod.Head);
        public static readonly HttpMethodCondition PatchInstance = new HttpMethodCondition(HttpMethod.Patch);
        public static readonly HttpMethodCondition TraceInstance = new HttpMethodCondition(HttpMethod.Trace);
        public static readonly HttpMethodCondition ConnectInstance = new HttpMethodCondition(HttpMethod.Connect);

        private readonly HttpMethod _method;

        public HttpMethodCondition(HttpMethod method)
        {
            _method = method;
        }

        public HttpRouteMatchResult Match(IHttpContext context)
            => context.Request.Method == _method
                ? HttpRouteMatchResult.Match
                : HttpRouteMatchResult.NoMatch;
    }
}
