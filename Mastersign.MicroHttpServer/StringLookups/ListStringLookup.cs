using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{Count} List Entries")]
    [DebuggerTypeProxy(typeof(StringLookupDebuggerProxy))]
    public class ListStringLookup : IStringLookup
    {
        private readonly IList<KeyValuePair<string, string>> _values;

        internal int Count => _values.Count;

        public ListStringLookup(IList<KeyValuePair<string, string>> values)
        {
            _values = values;
        }

        public string GetByName(string name) => _values
            .Where(kvp => kvp.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            .Select(kvp => kvp.Value)
            .First();

        public bool TryGetByName(string name, out string value)
        {
            value = _values
                .Where(kvp => kvp.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(kvp => kvp.Value)
                .FirstOrDefault();
            return value != default;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}