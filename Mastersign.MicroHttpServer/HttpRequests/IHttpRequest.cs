using System;
using System.Collections.Generic;
using System.IO;

namespace Mastersign.MicroHttpServer
{
    public interface IHttpRequest
    {
        string Protocol { get; }

        HttpMethod Method { get; }
        
        Uri Url { get; }

        IReadOnlyList<string> PathSegments { get; }

        IStringLookup Query { get; }
        
        IStringLookup Headers { get; }

        Stream ContentStream { get; }

        dynamic Content { get; set; }
    }
}
