using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;
using ActivityTracker.Application.Services;
using ActivityTracker.Web.Contracts.V1.Requests;
using ActivityTracker.Web.Contracts.V1.Responses;
using ActivityTracker.Web.Rest.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Web.Rest.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityRestController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityRestController(IActivityService activityService)
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

            var activity = await activityService.CreateActivityAsync(
                new Activity
                {
                    Name = createActivityRequest.Name
                },
                createActivityRequest.StartImmediately);

            var response = ToActivityResponse(activity);

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
            var activities = await activityService.GetActivitiesAsync();

            var response = new GetActivitiesResponse
            {
                Activities = activities.Select(ToActivityResponse)
            };

            return response.ToOk();
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartActivityAsync(ulong id)
        {
            var activity = await activityService.StartActivityAsync(id);

            if (activity == null)
            {
                var error = new ErrorResponseItem
                {
                    Message = $"Cannot find activity with id: {id}"
                };

                return error.ToNotFound();
            }

            var response = ToActivityResponse(activity);

            return response.ToOk();
        }

        [HttpPost("{id}/stop")]
        public async Task<IActionResult> StopActivityAsync(ulong id)
        {
            var activity = await activityService.StopActivityAsync(id);

            if (activity == null)
            {
                var error = new ErrorResponseItem
                {
                    Message = $"Cannot find activity with id: {id}"
                };

                return error.ToNotFound();
            }

            var response = ToActivityResponse(activity);

            return response.ToOk();
        }

        private static ActivityResponse ToActivityResponse(Activity activity) =>
            new()
            {
                Id = activity.Id,
                Name = activity.Name,
                Laps = activity.Laps.Select(ToLapResponse)
            };

        private static LapResponse ToLapResponse(Lap lap) =>
            new()
            {
                Id = lap.Id,
                StartDateTimeUtc = lap.StartDateTimeUtc,
                EndDateTimeUtc = lap.EndDateTimeUtc
            };
    }
}
