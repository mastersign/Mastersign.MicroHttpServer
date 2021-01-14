using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Empty")]
    public class EmptyStringLookup
        : IStringLookup
    {
        public static readonly IStringLookup Instance = new EmptyStringLookup();

        private static readonly IEnumerable<KeyValuePair<string, string>> EmptyKeyValuePairs 
            = Array.Empty<KeyValuePair<string, string>>();

        private EmptyStringLookup() { }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => EmptyKeyValuePairs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => EmptyKeyValuePairs.GetEnumerator();

        public string GetByName(string name) => throw new EntryNotFoundException("EmptyStringLookup does not contain any entry", name);

        public bool TryGetByName(string name, out string value)
        {
            value = null;
            return false;
        }
    }
}