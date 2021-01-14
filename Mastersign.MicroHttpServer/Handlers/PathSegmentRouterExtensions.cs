using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class PathSegmentRouterExtensions
    {
        public static PathSegmentRouter With(this PathSegmentRouter router,
            string pattern, Func<IHttpContext, IHttpResponse> method)
            => router.With(pattern, (ctx, _) => ctx.Respond(method(ctx)));

        public static PathSegmentRouter With(this PathSegmentRouter router,
            string pattern, Func<IHttpContext, Func<Task>, Task> method)
            => router.With(pattern, new AnonymousHttpRequestHandler(method));

        public static PathSegmentRouter With(this PathSegmentRouter router,
            string pattern, Func<IHttpContext, string> method, string contentType = "text/html; charset=utf-8")
            => router.With(pattern, new AnonymousStringHttpRequestHandler(method, contentType));

        public static PathSegmentRouter Without(this PathSegmentRouter router,
            Func<IHttpContext, IHttpResponse> method)
            => router.Without((ctx, _) => ctx.Respond(method(ctx)));

        public static PathSegmentRouter Without(this PathSegmentRouter router,
            Func<IHttpContext, Func<Task>, Task> method)
            => router.Without(new AnonymousHttpRequestHandler(method));

        public static PathSegmentRouter Without(this PathSegmentRouter router,
            Func<IHttpContext, string> method, string contentType = "text/html; charset=utf-8")
            => router.Without(new AnonymousStringHttpRequestHandler(method, contentType));

        public static PathSegmentRouter PathRouter(this PathSegmentRouter router, string pattern)
        {
            var childRouter = new PathSegmentRouter();
            router.With(pattern, childRouter);
            return childRouter;
        }
    }
}
