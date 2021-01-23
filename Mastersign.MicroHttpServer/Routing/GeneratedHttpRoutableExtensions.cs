using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class GeneratedHttpRoutableExtensions
    {
        private const string DEFAULT_STRING_MIMETYPE = "text/html; charset=utf-8";
        private const string DEFAULT_BYTE_ARRAY_MIMETYPE = "application/octet-stream";

        #region Unconditional

        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.Use(new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.Use((ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Branch

        public static IHttpRoutable Branch(this IHttpRoutable r, HttpMethod httpMethod)
            => r.Branch(new HttpMethodCondition(httpMethod));
        public static IHttpRoutable BranchRegex(this IHttpRoutable r, string regex)
            => r.Branch(new RegexRouteCondition(null, regex));
        public static IHttpRoutable BranchRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex)
            => r.Branch(new RegexRouteCondition(httpMethod, regex));
        public static IHttpRoutable Branch(this IHttpRoutable r, string pattern)
            => r.Branch(RegexRouteCondition.FromRoutePattern(null, pattern));
        public static IHttpRoutable Branch(this IHttpRoutable r, HttpMethod httpMethod, string pattern)
            => r.Branch(RegexRouteCondition.FromRoutePattern(httpMethod, pattern));

        #endregion

        #region EndWith

        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.EndWith(new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.EndWith((ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.EndWith(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.EndWith(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.EndWith(new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.EndWith(new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Condition pass through

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(condition, new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(condition, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(condition, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(condition, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(condition, new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(condition, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            IHttpRequestHandler handler)
            => r.UseWhen(new HttpMethodCondition(httpMethod), handler);
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new HttpMethodCondition(httpMethod), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Regex all HTTP methods

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(null, regex), handler);
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(null, regex), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(null, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Regex and HTTP method

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), handler);
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Route pattern all HTTP methods

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), handler);
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Route pattern and HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), handler);
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET All routes

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, handler);

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.GetInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET Regex

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), handler);

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET Route pattern

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), handler);

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST All routes

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, handler);

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PostInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST Regex

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), handler);

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST Route pattern

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), handler);

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT All routes

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, handler);

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PutInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT Regex

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), handler);

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT Route pattern

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), handler);

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH All routes

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, handler);

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PatchInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH Regex

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), handler);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH Route pattern

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), handler);

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE All routes

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, handler);

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE Regex

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), handler);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE Route pattern

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), handler);

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion


    }
}