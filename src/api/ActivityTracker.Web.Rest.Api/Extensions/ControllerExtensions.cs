using ActivityTracker.Web.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Web.Rest.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToBadRequest(this ErrorResponseItem error) =>
            new BadRequestObjectResult(new ResponseBody<object>(new[]
            {
                error
            }));

        public static IActionResult ToNotFound(this ErrorResponseItem error) =>
            new NotFoundObjectResult(new ResponseBody<object>(new[]
            {
                error
            }));

        public static IActionResult ToOk<T>(this T value) =>
            new OkObjectResult(new ResponseBody<T>(value));
    }
}