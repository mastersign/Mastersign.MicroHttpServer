using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mastersign.MicroHttpServer
{
    public static class StringLookupExtensions
    {
        public static IStringLookup ToStringLookup(this IEnumerable<KeyValuePair<string, string>> entries)
            => new DictionaryStringLookup(entries.ToDictionary(e => e.Key, e => e.Value, StringComparer.InvariantCultureIgnoreCase));

        public static bool TryGetByName<T>(this IStringLookup headers, string name, out T value)
        {
            if (headers.TryGetByName(name, out var stringValue))
            {
                value = (T)Convert.ChangeType(stringValue, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }

            value = default;
            return false;
        }

        public static T GetByName<T>(this IStringLookup headers, string name)
        {
            return headers.TryGetByName(name, out T value) 
                ? value 
                : throw new EntryNotFoundException($"Could not find '{name}' in lookup.", name);
        }

        public static T GetByNameOrDefault<T>(this IStringLookup headers, string name, T defaultValue)
        {
            return headers.TryGetByName(name, out T value) ? value : defaultValue;
        }

        public static string ToUriData(this IStringLookup headers)
        {
            var builder = new StringBuilder();
            foreach (var header in headers)
            {
                builder.AppendFormat("{0}={1}&", Uri.EscapeDataString(header.Key), Uri.EscapeDataString(header.Value));
            }
            return builder.ToString(0, builder.Length - 1);
        }
    }
}