using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;
using ActivityTracker.Application.Repositories;
using ActivityTracker.Data.Persistence.Extensions;
using ActivityTracker.Data.Persistence.Repositories.Dtos;

namespace ActivityTracker.Data.Persistence.Repositories
{
    /// <summary>
    /// Although we're not making use of async/await this would be implemented in the real persistence layer
    /// </summary>
    public class InMemoryActivityRepository : IActivityRepository
    {
        // todo: change to a persistent data store
        // todo: fix thread-safety issue, what if we have multiple requests modifying this dictionary simultaneously?
        private readonly SortedDictionary<ulong, ActivityDto> dataStore;

        public InMemoryActivityRepository(SortedDictionary<ulong, ActivityDto> dataStore = null)
        {
            this.dataStore = dataStore ?? new SortedDictionary<ulong, ActivityDto>();
        }

        public Task<Activity> CreateActivityAsync(Activity activity)
        {
            var activityDto = activity.ToActivityDto();

            activityDto.Id = GetNextId();

            dataStore.Add(activityDto.Id, activityDto);

            return Task.FromResult(activityDto.ToActivity());
        }

        public Task DeleteActivityAsync(ulong activityId)
        {
            dataStore.Remove(activityId);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return Task.FromResult(dataStore.Select(kvp => kvp.Value.ToActivity()));
        }

        public Task<Activity> GetActivityAsync(ulong activityId)
        {
            return dataStore.TryGetValue(activityId, out var activity)
                ? Task.FromResult(activity.ToActivity())
                : Task.FromResult((Activity) null);
        }

        public Task UpdateActivityAsync(Activity activity)
        {
            if (dataStore.ContainsKey(activity.Id))
            {
                dataStore[activity.Id] = activity.ToActivityDto();
            }

            return Task.CompletedTask;
        }

        private ulong GetNextId() => (dataStore.Count == 0 ? 0 : dataStore.Max(ds => ds.Key)) + 1;
    }
}
