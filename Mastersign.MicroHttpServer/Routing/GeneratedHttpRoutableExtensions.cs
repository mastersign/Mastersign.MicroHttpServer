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
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.Use(async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable Use(this IHttpRoutable r, byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Branch

        public static IHttpRoutable Branch(this IHttpRoutable r, HttpMethod httpMethod)
            => r.Branch(new HttpMethodCondition(httpMethod));
        public static IHttpRoutable BranchRegex(this IHttpRoutable r, string regex)
            => r.Branch(new RegexRouteCondition(regex, rightOpen: true));
        public static IHttpRoutable BranchRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex)
            => r.Branch(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: true)));
        public static IHttpRoutable Branch(this IHttpRoutable r, string pattern)
            => r.Branch(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: true));
        public static IHttpRoutable Branch(this IHttpRoutable r, HttpMethod httpMethod, string pattern)
            => r.Branch(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: true)));

        #endregion

        #region EndWith

        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.EndWith(new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.EndWith((ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.EndWith(async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.EndWith(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.EndWith(new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.EndWith(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable EndWith(this IHttpRoutable r, Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.EndWith(new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
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
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(condition, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(condition, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(condition, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(condition, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(condition, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
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
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new HttpMethodCondition(httpMethod), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
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
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), handler);
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(regex, rightOpen: false), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Regex and HTTP method

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), handler);
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r,
            HttpMethod httpMethod, string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), new RegexRouteCondition(regex, rightOpen: false)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Route pattern all HTTP methods

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), handler);
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region Route pattern and HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), handler);
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new AnonymousHttpRequestHandler(handler));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new ConstStringHttpRequestHandler(text, contentType: contentType));
        public static IHttpRoutable UseWhen(this IHttpRoutable r,
            HttpMethod httpMethod, string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(new HttpMethodCondition(httpMethod), RegexRouteCondition.FromRoutePattern(pattern, rightOpen: false)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET All routes

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, handler);

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.GetInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(HttpMethodCondition.GetInstance, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable GetAll(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET Condition pass through

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), handler);

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, condition), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET Regex

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), handler);

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable GetRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, new RegexRouteCondition(regex)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region GET Route pattern

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), handler);

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Get(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.GetInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST All routes

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, handler);

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PostInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(HttpMethodCondition.PostInstance, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PostAll(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST Condition pass through

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), handler);

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, condition), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST Regex

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), handler);

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PostRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, new RegexRouteCondition(regex)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region POST Route pattern

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), handler);

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Post(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PostInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT All routes

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, handler);

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PutInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(HttpMethodCondition.PutInstance, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PutAll(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT Condition pass through

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), handler);

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, condition), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT Regex

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), handler);

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PutRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, new RegexRouteCondition(regex)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PUT Route pattern

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), handler);

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Put(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PutInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH All routes

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, handler);

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PatchInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(HttpMethodCondition.PatchInstance, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PatchAll(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH Condition pass through

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), handler);

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, condition), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH Regex

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), handler);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable PatchRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, new RegexRouteCondition(regex)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region PATCH Route pattern

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), handler);

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Patch(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.PatchInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE All routes

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, handler);

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable DeleteAll(this IHttpRoutable r,
            
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE Condition pass through

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), handler);

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            IHttpRouteCondition condition, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, condition), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE Regex

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), handler);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r,
            string regex, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, new RegexRouteCondition(regex)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion

        #region DELETE Route pattern

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            IHttpRequestHandler handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), handler);

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), (ctx, _) => { ctx.Response = responseGenerator(ctx); return Task.Factory.GetCompleted(); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<IHttpResponse>> asyncResponseGenerator)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), async (ctx, _) => { ctx.Response = await asyncResponseGenerator(ctx); });

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<string>> asyncContentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousStringHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            Func<IHttpContext, Task<byte[]>> asyncContentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new AsyncAnonymousByteArrayHttpRequestHandler(asyncContentGenerator, contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            string text, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstStringHttpRequestHandler(text, contentType: contentType));

        public static IHttpRoutable Delete(this IHttpRoutable r,
            string pattern, 
            byte[] data, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new CombinedRouteCondition(HttpMethodCondition.DeleteInstance, RegexRouteCondition.FromRoutePattern(pattern)), new ConstByteArrayHttpRequestHandler(data, contentType: contentType));

        #endregion


    }
}