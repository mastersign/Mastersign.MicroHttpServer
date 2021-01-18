using System;

namespace Mastersign.MicroHttpServer
{
    internal static class UriExtensions
    {
        private static readonly char[] SEPARATORS = { '/' };

        internal static string[] GetPathSegments(this Uri uri)
            => uri.AbsolutePath.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
    }
}
