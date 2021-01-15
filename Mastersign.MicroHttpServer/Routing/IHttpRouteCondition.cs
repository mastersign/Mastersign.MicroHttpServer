namespace Mastersign.MicroHttpServer
{
    public interface IHttpRouteCondition
    {
        HttpRouteMatchResult Match(IHttpContext context);
    }
}
