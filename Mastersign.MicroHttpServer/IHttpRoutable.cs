namespace Mastersign.MicroHttpServer
{
    public interface IHttpRoutable
    {
        IHttpRoutable Use(IHttpRequestHandler handler);

        IHttpRoutable UseWhen(IHttpRouteCondition condition, IHttpRequestHandler handler);

        IHttpRoutable Dive(IHttpRouteCondition condition);

        IHttpRoutable Ascent(IHttpRequestHandler fallback = null);
    }
}
