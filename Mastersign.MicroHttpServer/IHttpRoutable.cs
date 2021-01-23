namespace Mastersign.MicroHttpServer
{
    public interface IHttpRoutable
    {
        string Name { get; }

        IHttpRoutable Use(IHttpRequestHandler handler);

        IHttpRoutable UseWhen(IHttpRouteCondition condition, IHttpRequestHandler handler);

        IHttpRoutable Branch(IHttpRouteCondition condition, string name = "branch");

        IHttpRoutable EndWith(IHttpRequestHandler fallback);

        IHttpRoutable Merge();
    }
}
