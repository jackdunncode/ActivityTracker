using System.Threading.Tasks;
using ActivityTracker.Web.Contracts.V1.Requests;
using ActivityTracker.Web.Contracts.V1.Responses;

namespace ActivityTracker.Web.Api.Services
{
    public interface IActivityService
    {
        Task<CreateActivityResponse> CreateActivityAsync(CreateActivityRequest createActivityRequest);
        Task DeleteActivityAsync(ulong activityId);
        Task<GetActivitiesResponse> GetActivitiesAsync();
        Task<StartActivityResponse> StartActivityAsync(ulong activityId);
        Task<StopActivityResponse> StopActivityAsync(ulong activityId);
    }
}
