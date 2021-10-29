using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Data.Repositories.Dtos;

namespace ActivityTracker.Data.Repositories
{
    /// <summary>
    /// Although we're not making use of async/await this would be implemented in the real persistence layer
    /// </summary>
    public class InMemoryActivityRepository : IActivityRepository
    {
        // todo: change to a persistent data store
        // todo: fix thread-safety issue, what if we have multiple requests modifying this dictionary simultaneously?
        private readonly SortedDictionary<ulong, ActivityDto> dataStore;

        public InMemoryActivityRepository()
        {
            dataStore = new SortedDictionary<ulong, ActivityDto>();
        }

        public Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto)
        {
            activityDto.Id = GetNextId();

            dataStore.Add(activityDto.Id, activityDto);

            return Task.FromResult(activityDto);
        }

        public Task DeleteActivityAsync(ulong activityId)
        {
            dataStore.Remove(activityId);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ActivityDto>> GetActivitiesAsync()
        {
            return Task.FromResult(dataStore.Select(kvp => kvp.Value));
        }

        public Task<ActivityDto> GetActivityAsync(ulong activityId)
        {
            return dataStore.TryGetValue(activityId, out var activityDto)
                ? Task.FromResult(activityDto)
                : Task.FromResult((ActivityDto) null);
        }

        public Task UpdateActivityAsync(ActivityDto activityDto)
        {
            if (dataStore.ContainsKey(activityDto.Id))
            {
                dataStore[activityDto.Id] = activityDto;
            }

            return Task.CompletedTask;
        }

        private ulong GetNextId() => (dataStore.Count == 0 ? 0 : dataStore.Max(ds => ds.Key)) + 1;
    }
}
