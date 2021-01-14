using System.Collections.Generic;

namespace Mastersign.MicroHttpServer
{
    public interface IStringLookup


        : IEnumerable<KeyValuePair<string, string>>
    {
        string GetByName(string name);

        bool TryGetByName(string name, out string value);
    }
}