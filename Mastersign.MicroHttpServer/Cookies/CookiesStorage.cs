using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mastersign.MicroHttpServer
{
    public class CookiesStorage : ICookiesStorage
    {
        private const string HTTP_NEWLINE = "\r\n";
        private static readonly string[] COOKIE_SEPARATORS = { "; ", "=" };

        private readonly Dictionary<string, string> _values;

        public CookiesStorage(string cookies)
        {
            var keyValues = cookies.Split(COOKIE_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
            _values = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            for (var i = 0; i < keyValues.Length; i += 2)
            {
                var key = keyValues[i];
                var value = keyValues[i + 1];

                _values[key] = value;
            }
        }

        public bool Touched { get; private set; }

        public string ToCookieData()
        {
            var builder = new StringBuilder();

            foreach (var kvp in _values)
            {
                builder.AppendFormat("Set-Cookie: {0}={1}", kvp.Key, kvp.Value);
                builder.Append(HTTP_NEWLINE);
            }

            return builder.ToString();
        }

        public void Upsert(string key, string value)
        {
            _values[key] = value;

            Touched = true;
        }

        public void Remove(string key)
        {
            if (_values.Remove(key)) Touched = true;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string GetByName(string name) => _values[name];

        public bool TryGetByName(string name, out string value) => _values.TryGetValue(name, out value);
    }
}