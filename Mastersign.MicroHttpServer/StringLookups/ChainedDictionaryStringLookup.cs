using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{Count} Chained Dictionary Entries")]
    [DebuggerTypeProxy(typeof(StringLookupDebuggerProxy))]
    public class ChainedDictionaryStringLookup
        : IStringLookup
    {
        private readonly IStringLookup _parent;
        private readonly IDictionary<string, string> _values;

        internal int Count => Keys.Count();

        public ChainedDictionaryStringLookup(IStringLookup parent, IDictionary<string, string> values)
        {
            _parent = parent;
            _values = values;
        }

        private IEnumerable<string> Keys => _values.Keys.Concat(_parent.Select(kvp => kvp.Key)).Distinct();

        public string GetByName(string name) 
            => _values.TryGetValue(name, out var value)
                ? value
                : _parent.GetByName(name);

        public bool TryGetByName(string name, out string value) 
            => _values.TryGetValue(name, out value)
                || _parent.TryGetByName(name, out value);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => Keys
            .Select(name => new KeyValuePair<string, string>(name, GetByName(name)))
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
