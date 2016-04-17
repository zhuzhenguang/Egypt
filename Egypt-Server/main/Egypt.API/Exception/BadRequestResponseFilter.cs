using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Egypt.API.Exception
{
    public class BadRequestResponseFilter : ExceptionFilterAttribute
    {
        static readonly List<Type> WatchedExceptions = new List<Type>
        {
            typeof (BadRequestException)
        };

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            if (!IsWatched(exception)) return;

            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest,
                exception.Message);
        }

        static bool IsWatched(System.Exception exception)
        {
            return WatchedExceptions.Any(t => t.IsInstanceOfType(exception));
        }
    }
}