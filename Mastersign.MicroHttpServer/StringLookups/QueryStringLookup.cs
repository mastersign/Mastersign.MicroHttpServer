using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{Count} Query Entries")]
    [DebuggerTypeProxy(typeof(StringLookupDebuggerProxy))]
    internal class QueryStringLookup
        : IStringLookup
    {
        private static readonly char[] _seperators = { '&', '=' };
        private readonly DictionaryStringLookup _child;

        internal int Count => _child.Count;

        public QueryStringLookup(string query)
        {
            var splittedKeyValues = query.Split(_seperators, StringSplitOptions.RemoveEmptyEntries);
            var values = new Dictionary<string, string>(splittedKeyValues.Length / 2, StringComparer.InvariantCultureIgnoreCase);

            for (var i = 0; i < splittedKeyValues.Length; i += 2)
            {
                var key = Uri.UnescapeDataString(splittedKeyValues[i]);
                string value = null;
                if (splittedKeyValues.Length > i + 1) value = Uri.UnescapeDataString(splittedKeyValues[i + 1]).Replace('+', ' ');

                values[key] = value;
            }
            _child = new DictionaryStringLookup(values);
        }

        public string GetByName(string name) => _child.GetByName(name);

        public bool TryGetByName(string name, out string value) => _child.TryGetByName(name, out value);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _child.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}