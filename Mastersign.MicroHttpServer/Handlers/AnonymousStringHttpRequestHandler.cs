﻿using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class AnonymousStringHttpRequestHandler : IHttpRequestHandler
    {
        private readonly Func<IHttpContext, string> _method;
        private readonly string _contentType;

        public AnonymousStringHttpRequestHandler(Func<IHttpContext, string> method, string contentType = "text/html; charset=utf-8")
        {
            _method = method;
            _contentType = contentType;
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            var result = _method(context);
            if (result != null)
            {
                context.Response = StringHttpResponse.Create(result, contentType: _contentType);
                return Task.Factory.GetCompleted();
            }

            return next();
        }
    }

}
