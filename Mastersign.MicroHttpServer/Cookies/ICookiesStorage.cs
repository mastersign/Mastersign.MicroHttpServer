namespace Mastersign.MicroHttpServer
{
    public interface ICookiesStorage : IStringLookup
    {
        bool Touched { get; }

        void Upsert(string key, string value);

        void Remove(string key);

        string ToCookieData();
    }
}