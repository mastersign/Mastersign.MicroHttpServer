using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mastersign.MicroHttpServer
{
    internal class StringLookupDebuggerProxy

    {
        private readonly IStringLookup _real;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public HttpHeader[] Headers => _real.Select(kvp => new HttpHeader(kvp)).ToArray();

        public StringLookupDebuggerProxy(IStringLookup real)
        {
            _real = real;
        }

        [DebuggerDisplay("{Value,nq}", Name = "{Key,nq}")]
        internal class HttpHeader
        {
            private readonly KeyValuePair<string, string> _header;

            public string Value => _header.Value;

            public string Key => _header.Key;

            public HttpHeader(KeyValuePair<string, string> header)
            {
                _header = header;
            }
        }
    }
}