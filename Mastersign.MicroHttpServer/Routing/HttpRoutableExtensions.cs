using System;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class HttpRoutableExtensions
    {
        private const string DEFAULT_STRING_MIMETYPE = "text/html; charset=utf-8";
        private const string DEFAULT_BYTE_ARRAY_MIMETYPE = "application/octet-stream";


        #region Unconditional

        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.Use(new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.Use((ctx, _) =>
            {
                ctx.Response = responseGenerator(ctx);
                return Task.Factory.GetCompleted();
            });

        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Use(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Use(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Use(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        #endregion

        #region Dive

        public static IHttpRoutable DiveRegex(this IHttpRoutable r, string regex)
            => r.Dive(new RegexRouteCondition(null, regex, rightOpen: true));

        public static IHttpRoutable Dive(this IHttpRoutable r, string pattern)
            => r.Dive(RegexRouteCondition.FromRoutePattern(null, pattern, rightOpen: true));

        public static IHttpRoutable DiveRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex)
            => r.Dive(new RegexRouteCondition(httpMethod, regex, rightOpen: true));

        public static IHttpRoutable Dive(this IHttpRoutable r, HttpMethod httpMethod, string pattern)
            => r.Dive(RegexRouteCondition.FromRoutePattern(httpMethod, pattern, rightOpen: true));

        public static IHttpRoutable Dive(this IHttpRoutable r, HttpMethod httpMethod)
            => r.Dive(new HttpMethodCondition(httpMethod));

        #endregion

        #region Ascent

        public static IHttpRoutable Ascent(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.Ascent(new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable Ascent(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.Ascent((ctx, _) =>
            {
                ctx.Response = responseGenerator(ctx);
                return Task.Factory.GetCompleted();
            });

        public static IHttpRoutable Ascent(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.Ascent(new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable Ascent(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.Ascent(new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        #endregion

        #region Conditional

        public static IHttpRoutable UseWhen(this IHttpRoutable r, IHttpRouteCondition condition, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(condition, new AnonymousHttpRequestHandler(handler));

        public static IHttpRoutable UseWhen(this IHttpRoutable r, IHttpRouteCondition condition, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(condition, (ctx, _) =>
            {
                ctx.Response = responseGenerator(ctx);
                return Task.Factory.GetCompleted();
            });

        public static IHttpRoutable UseWhen(this IHttpRoutable r, IHttpRouteCondition condition, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(condition, new AnonymousStringHttpRequestHandler(contentGenerator, contentType));

        public static IHttpRoutable UseWhen(this IHttpRoutable r, IHttpRouteCondition condition, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(condition, new AnonymousByteArrayHttpRequestHandler(contentGenerator, contentType));

        #endregion

        #region HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, IHttpRequestHandler handler)
            => r.UseWhen(new HttpMethodCondition(httpMethod), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new HttpMethodCondition(httpMethod), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new HttpMethodCondition(httpMethod), responseGenerator);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), contentGenerator, contentType);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new HttpMethodCondition(httpMethod), contentGenerator, contentType);

        #endregion

        #region Regex pattern no HTTP method

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(null, regex), handler);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(null, regex), handler);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(null, regex), responseGenerator);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), contentGenerator, contentType);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(null, regex), contentGenerator, contentType);

        #endregion

        #region Regex pattern with HTTP method

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), handler);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), handler);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), responseGenerator);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), contentGenerator, contentType);

        public static IHttpRoutable UseWhenRegex(this IHttpRoutable r, HttpMethod httpMethod, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(httpMethod, regex), contentGenerator, contentType);

        #endregion

        #region Simple pattern no HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), responseGenerator);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), contentGenerator, contentType);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(null, pattern), contentGenerator, contentType);

        #endregion

        #region Simple pattern with HTTP method

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), handler);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), responseGenerator);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), contentGenerator, contentType);

        public static IHttpRoutable UseWhen(this IHttpRoutable r, HttpMethod httpMethod, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(httpMethod, pattern), contentGenerator, contentType);

        #endregion

        #region GET Regex pattern

        public static IHttpRoutable GetRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), handler);

        public static IHttpRoutable GetRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), handler);

        public static IHttpRoutable GetRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), responseGenerator);

        public static IHttpRoutable GetRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), contentGenerator, contentType);

        public static IHttpRoutable GetRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Get, regex), contentGenerator, contentType);

        #endregion

        #region GET simple pattern

        public static IHttpRoutable Get(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), handler);

        public static IHttpRoutable Get(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), handler);

        public static IHttpRoutable Get(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), responseGenerator);

        public static IHttpRoutable Get(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), contentGenerator, contentType);

        public static IHttpRoutable Get(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Get, pattern), contentGenerator, contentType);

        #endregion

        #region GET no pattern

        public static IHttpRoutable Get(this IHttpRoutable r, IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, handler);

        public static IHttpRoutable Get(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.GetInstance, handler);

        public static IHttpRoutable Get(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.GetInstance, responseGenerator);

        public static IHttpRoutable Get(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, contentGenerator, contentType);

        public static IHttpRoutable Get(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.GetInstance, contentGenerator, contentType);

        #endregion

        #region POST Regex pattern

        public static IHttpRoutable PostRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), handler);

        public static IHttpRoutable PostRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), handler);

        public static IHttpRoutable PostRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), responseGenerator);

        public static IHttpRoutable PostRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), contentGenerator, contentType);

        public static IHttpRoutable PostRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Post, regex), contentGenerator, contentType);

        #endregion

        #region POST simple pattern

        public static IHttpRoutable Post(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), handler);

        public static IHttpRoutable Post(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), handler);

        public static IHttpRoutable Post(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), responseGenerator);

        public static IHttpRoutable Post(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), contentGenerator, contentType);

        public static IHttpRoutable Post(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Post, pattern), contentGenerator, contentType);

        #endregion

        #region POST no pattern

        public static IHttpRoutable Post(this IHttpRoutable r, IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, handler);

        public static IHttpRoutable Post(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PostInstance, handler);

        public static IHttpRoutable Post(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PostInstance, responseGenerator);

        public static IHttpRoutable Post(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, contentGenerator, contentType);

        public static IHttpRoutable Post(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PostInstance, contentGenerator, contentType);

        #endregion

        #region PUT Regex pattern

        public static IHttpRoutable PutRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), handler);

        public static IHttpRoutable PutRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), handler);

        public static IHttpRoutable PutRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), responseGenerator);

        public static IHttpRoutable PutRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), contentGenerator, contentType);

        public static IHttpRoutable PutRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Put, regex), contentGenerator, contentType);

        #endregion

        #region PUT simple pattern

        public static IHttpRoutable Put(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), handler);

        public static IHttpRoutable Put(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), handler);

        public static IHttpRoutable Put(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), responseGenerator);

        public static IHttpRoutable Put(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), contentGenerator, contentType);

        public static IHttpRoutable Put(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Put, pattern), contentGenerator, contentType);

        #endregion

        #region PUT no pattern

        public static IHttpRoutable Put(this IHttpRoutable r, IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, handler);

        public static IHttpRoutable Put(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PutInstance, handler);

        public static IHttpRoutable Put(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PutInstance, responseGenerator);

        public static IHttpRoutable Put(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, contentGenerator, contentType);

        public static IHttpRoutable Put(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PutInstance, contentGenerator, contentType);

        #endregion

        #region PATCH Regex pattern

        public static IHttpRoutable PatchRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), handler);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), handler);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), responseGenerator);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), contentGenerator, contentType);

        public static IHttpRoutable PatchRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Patch, regex), contentGenerator, contentType);

        #endregion

        #region PATCH simple pattern

        public static IHttpRoutable Patch(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), handler);

        public static IHttpRoutable Patch(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), handler);

        public static IHttpRoutable Patch(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), responseGenerator);

        public static IHttpRoutable Patch(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), contentGenerator, contentType);

        public static IHttpRoutable Patch(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Patch, pattern), contentGenerator, contentType);

        #endregion

        #region PATCH no pattern

        public static IHttpRoutable Patch(this IHttpRoutable r, IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, handler);

        public static IHttpRoutable Patch(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.PatchInstance, handler);

        public static IHttpRoutable Patch(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.PatchInstance, responseGenerator);

        public static IHttpRoutable Patch(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, contentGenerator, contentType);

        public static IHttpRoutable Patch(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.PatchInstance, contentGenerator, contentType);

        #endregion

        #region DELETE Regex pattern

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r, string regex, IHttpRequestHandler handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), handler);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r, string regex, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), handler);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r, string regex, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), responseGenerator);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r, string regex, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), contentGenerator, contentType);

        public static IHttpRoutable DeleteRegex(this IHttpRoutable r, string regex, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(new RegexRouteCondition(HttpMethod.Delete, regex), contentGenerator, contentType);

        #endregion

        #region DELETE simple pattern

        public static IHttpRoutable Delete(this IHttpRoutable r, string pattern, IHttpRequestHandler handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), handler);

        public static IHttpRoutable Delete(this IHttpRoutable r, string pattern, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), handler);

        public static IHttpRoutable Delete(this IHttpRoutable r, string pattern, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), responseGenerator);

        public static IHttpRoutable Delete(this IHttpRoutable r, string pattern, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), contentGenerator, contentType);

        public static IHttpRoutable Delete(this IHttpRoutable r, string pattern, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(RegexRouteCondition.FromRoutePattern(HttpMethod.Delete, pattern), contentGenerator, contentType);

        #endregion

        #region DELETE no pattern

        public static IHttpRoutable Delete(this IHttpRoutable r, IHttpRequestHandler handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, handler);

        public static IHttpRoutable Delete(this IHttpRoutable r, Func<IHttpContext, Func<Task>, Task> handler)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, handler);

        public static IHttpRoutable Delete(this IHttpRoutable r, Func<IHttpContext, IHttpResponse> responseGenerator)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, responseGenerator);

        public static IHttpRoutable Delete(this IHttpRoutable r, Func<IHttpContext, string> contentGenerator, string contentType = DEFAULT_STRING_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, contentGenerator, contentType);

        public static IHttpRoutable Delete(this IHttpRoutable r, Func<IHttpContext, byte[]> contentGenerator, string contentType = DEFAULT_BYTE_ARRAY_MIMETYPE)
            => r.UseWhen(HttpMethodCondition.DeleteInstance, contentGenerator, contentType);

        #endregion
    }
}
