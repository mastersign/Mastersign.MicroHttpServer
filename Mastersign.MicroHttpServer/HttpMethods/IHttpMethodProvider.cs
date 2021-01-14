namespace Mastersign.MicroHttpServer
{
    public interface IHttpMethodProvider
    {
        HttpMethod Provide(string name);
    }
}