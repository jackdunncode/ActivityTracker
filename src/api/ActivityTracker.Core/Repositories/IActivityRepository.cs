using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Core.Repositories.Dtos;

namespace ActivityTracker.Core.Repositories
{
    public interface IActivityRepository
    {
        Task<ActivityDto> CreateActivityAsync(ActivityDto activity);
        Task DeleteActivityAsync(ulong activityId);
        Task<IEnumerable<ActivityDto>> GetActivitiesAsync();
        Task<ActivityDto> GetActivityAsync(ulong activityId);
        Task UpdateActivityAsync(ActivityDto activityDto);
    }
}
