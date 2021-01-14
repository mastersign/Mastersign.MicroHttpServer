using System;
using System.Collections.Generic;
using System.IO;

namespace Mastersign.MicroHttpServer
{
    internal class HttpRequestMethodDecorator : IHttpRequest
    {
        private readonly IHttpRequest _child;

        public HttpRequestMethodDecorator(IHttpRequest child, HttpMethod method)
        {
            _child = child;
            Method = method;
        }

        public HttpMethod Method { get; }

        public Uri Url => _child.Url;

        public string Protocol => _child.Protocol;

        public IReadOnlyList<string> PathSegments => _child.PathSegments;

        public IStringLookup Query => _child.Query;
        
        public IStringLookup Headers => _child.Headers;

        public Stream ContentStream => _child.ContentStream;

        public dynamic Content
        {
            get => _child.Content;
            set => _child.Content = value;
        }
    }
}