using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("{Method} {Url.PathAndQuery,nq}")]
    internal class HttpRequest : IHttpRequest
    {
        public HttpRequest(
            HttpMethod method,
            Uri uri,
            string protocol,
            IStringLookup headers,
            Stream contentStream)
        {
            Method = method;
            Url = uri;
            Protocol = protocol;
            PathSegments = uri.GetPathSegments();
            Headers = headers;
            Query = string.IsNullOrEmpty(uri.Query)
                ? EmptyStringLookup.Instance
                : new QueryStringLookup(uri.Query);
            ;
            ContentStream = contentStream;
            Content = null;
        }

        public IStringLookup Headers { get; }

        public HttpMethod Method { get; }

        public Uri Url { get; }

        public string Protocol { get; }

        public IReadOnlyList<string> PathSegments { get; }

        public IStringLookup Query { get; }

        public Stream ContentStream { get; }

        public dynamic Content { get; set; }
    }
}