﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Const String Handler: {Response.ResponseCode}, {Text.Length} Chars, {ContentType}")]
    public class ConstStringHttpRequestHandler : IHttpRequestHandler
    {
        public string Text { get; }
        
        public string ContentType { get; }

        public IHttpResponse Response { get; }

        public ConstStringHttpRequestHandler(string text,
            HttpResponseCode code = HttpResponseCode.OK,
            string contentType = "text/html; charset=utf-8", 
            bool keepALive = true)
        {
            Text = text;
            ContentType = contentType;
            Response = StringHttpResponse.Create(text,
                contentType: contentType,
                code: code,
                keepAlive: keepALive);
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            context.Response = Response;
            return Task.Factory.GetCompleted();
        }
    }
}
