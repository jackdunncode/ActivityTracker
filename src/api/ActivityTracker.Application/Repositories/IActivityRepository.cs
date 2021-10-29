using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;

namespace ActivityTracker.Application.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> CreateActivityAsync(Activity activity);
        Task DeleteActivityAsync(ulong activityId);
        Task<IEnumerable<Activity>> GetActivitiesAsync();
        Task<Activity> GetActivityAsync(ulong activityId);
        Task UpdateActivityAsync(Activity activity);
    }
}
