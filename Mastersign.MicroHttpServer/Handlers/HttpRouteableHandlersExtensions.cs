namespace Mastersign.MicroHttpServer.Handlers
{
    public static class HttpRouteableHandlersExtensions
    {
        public static IHttpRoutable StaticFiles(this IHttpRoutable r, string rootDirectory = ".")
            => r.Use(new FileHandler(rootDirectory));

        public static IHttpRoutable StaticFiles(this IHttpRoutable r, string pattern, string rootDirectory = ".")
            => r.Branch(pattern).StaticFiles(rootDirectory).EndWith(new NotFoundHandler());
    }
}
