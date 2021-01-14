using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class HttpContextExtensions
    {
        public static Task Redirect(this IHttpContext context, HttpResponseCode code, string location, bool keepAlive = true)
        {
            context.Response = RedirectingHttpResponse.Create(location, code, keepAlive);
            return Task.Factory.GetCompleted();
        }

        public static Task PointToOtherLocation(this IHttpContext context, string location, bool keepAlive = true)
            => context.Redirect(HttpResponseCode.SeeOther, location, keepAlive);

        public static Task RedirectTemporarily(this IHttpContext context, string location, bool keepAlive = true)
            => context.Redirect(HttpResponseCode.TemporaryRedirect, location, keepAlive);

        public static Task RedirectPermantly(this IHttpContext context, string location, bool keepAlive = true)
            => context.Redirect(HttpResponseCode.PermanentRedirect, location, keepAlive);

        public static Task NotifyMovedResource(this IHttpContext context, string location, bool keepAlive = true)
            => context.Redirect(HttpResponseCode.MovedPermanently, location, keepAlive);

        public static Task Respond(this IHttpContext context, IHttpResponse response)
        {
            context.Response = response;
            return Task.Factory.GetCompleted();
        }

        public static Task RespondStatus(this IHttpContext context, HttpResponseCode code, string message = null, bool keepAlive = true) 
            => context.Respond(code == HttpResponseCode.NoContent
                ? (IHttpResponse)EmptyHttpResponse.Create(code, keepAlive)
                : StringHttpResponse.Text(
                    message != null
                        ? code + Environment.NewLine + Environment.NewLine + message
                        : code.ToString(),
                    code, keepAlive));

        public static Task RespondNoContent(this IHttpContext context, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.NoContent, keepAlive: keepAlive);

        public static Task RespondNotFound(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.NotFound, message, keepAlive);

        public static Task RespondNotImplemnented(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.NotImplemented, message, keepAlive);

        public static Task RespondBadRequest(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.BadRequest, message, keepAlive);

        public static Task RespondInternalServerError(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.InternalServerError, message, keepAlive);

        public static Task RespondCreated(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.Created, message, keepAlive);

        public static Task RespondConflict(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.Conflict, message, keepAlive);

        public static Task RespondUnauthorized(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.Unauthorized, message, keepAlive);

        public static Task RespondForbidden(this IHttpContext context, string message = null, bool keepAlive = true)
            => context.RespondStatus(HttpResponseCode.Forbidden, message, keepAlive);

    }
}
