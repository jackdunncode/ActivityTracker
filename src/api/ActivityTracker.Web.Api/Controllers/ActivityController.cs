using System.Threading.Tasks;
using ActivityTracker.Web.Api.Extensions;
using ActivityTracker.Web.Api.Services;
using ActivityTracker.Web.Contracts.V1.Requests;
using ActivityTracker.Web.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivityAsync(CreateActivityRequest createActivityRequest)
        {
            if (string.IsNullOrWhiteSpace(createActivityRequest?.Name))
            {
                var error = new ErrorResponseItem
                {
                    Message = "Activity name is not specified"
                };

                return error.ToBadRequest();
            }

            var response = await activityService.CreateActivityAsync(createActivityRequest);

            return response.ToOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityAsync(ulong id)
        {
            await activityService.DeleteActivityAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetActivitiesAsync()
        {
            var response = await activityService.GetActivitiesAsync();

            return response.ToOk();
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartActivityAsync(ulong id)
        {
            var response = await activityService.StartActivityAsync(id);

            if (response.Activity == null)
            {
                var error = new ErrorResponseItem
                {
                    Message = $"Cannot find activity with id: {id}"
                };

                return error.ToNotFound();
            }

            return response.ToOk();
        }

        [HttpPost("{id}/stop")]
        public async Task<IActionResult> StopActivityAsync(ulong id)
        {
            var response = await activityService.StopActivityAsync(id);

            if (response.Activity == null)
            {
                var error = new ErrorResponseItem
                {
                    Message = $"Cannot find activity with id: {id}"
                };

                return error.ToNotFound();
            }

            return response.ToOk();
        }
    }
}
