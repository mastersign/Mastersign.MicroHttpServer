using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{Count} Dictionary Entries")]
    [DebuggerTypeProxy(typeof(StringLookupDebuggerProxy))]
    public class DictionaryStringLookup
        : IStringLookup
    {
        private readonly IDictionary<string, string> _values;

        internal int Count => _values.Count;

        public DictionaryStringLookup(IDictionary<string, string> values)
        {
            _values = values;
        }

        public string GetByName(string name) => _values[name];

        public bool TryGetByName(string name, out string value) => _values.TryGetValue(name, out value);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}