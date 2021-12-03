using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ActivityTracker.Web.Contracts.V1.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ActivityTracker.Web.Rest.Api.Middleware
{
    public static class JsonExceptionMiddleware
    {
        public static async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var responseBody = new ResponseBody<object>(new List<ErrorResponseItem>
            {
                new ErrorResponseItem
                {
                    Message = ex.Message
                }
            });

            context.Response.ContentType = "application/json";

            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, responseBody);
        }
    }
}