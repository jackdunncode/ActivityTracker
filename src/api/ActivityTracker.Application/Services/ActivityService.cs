using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;
using ActivityTracker.Application.Repositories;

namespace ActivityTracker.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public ActivityService(IActivityRepository activityRepository, IDateTimeProvider dateTimeProvider)
        {
            this.activityRepository = activityRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<Activity> CreateActivityAsync(Activity createActivityRequest, bool startImmediately)
        {
            var laps = startImmediately
                ? new Lap[]
                {
                    new Lap
                    {
                        Id = 1,
                        StartDateTimeUtc = dateTimeProvider.GetDateTimeUtc()
                    }
                }
                : Enumerable.Empty<Lap>();

            var activity = new Activity
            {
                Name = createActivityRequest.Name,
                Laps = laps
            };

            var result = await activityRepository.CreateActivityAsync(activity);

            return result;
        }

        public Task DeleteActivityAsync(ulong activityId)
        {
            return activityRepository.DeleteActivityAsync(activityId);
        }

        public Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return activityRepository.GetActivitiesAsync();
        }

        public async Task<Activity> StartActivityAsync(ulong activityId)
        {
            var activity = await activityRepository.GetActivityAsync(activityId);
            if (activity == null)
                return new Activity();

            ulong newLapId = 1;

            var laps = activity.Laps.ToArray();
            if (laps.Length > 0)
            {
                var lastLap = laps.Last();
                if (lastLap.StartDateTimeUtc.HasValue && lastLap.EndDateTimeUtc.HasValue)
                {
                    newLapId = lastLap.Id + 1;
                } else if (!lastLap.EndDateTimeUtc.HasValue)
                {
                    // Ideally a domain model should be returned encapsulating any errors as throwing exceptions are inefficient
                    throw new Exception("Cannot start activity as it has already started");
                }
            }

            var newLap = new Lap
            {
                Id = newLapId,
                StartDateTimeUtc = dateTimeProvider.GetDateTimeUtc()
            };

            activity.Laps = activity.Laps.Concat(new[] {newLap});

            await activityRepository.UpdateActivityAsync(activity);

            return activity;
        }

        public async Task<Activity> StopActivityAsync(ulong activityId)
        {
            var activity = await activityRepository.GetActivityAsync(activityId);
            if (activity == null)
                return new Activity();

            var laps = activity.Laps.ToArray();
            if (laps.Length == 0)
            {
                throw new Exception("Cannot stop activity as it has not yet started");
            }

            var lastLap = laps.Last();
            if (lastLap.EndDateTimeUtc.HasValue)
            {
                throw new Exception("Cannot stop activity as it has already stopped");
            }

            lastLap.EndDateTimeUtc = dateTimeProvider.GetDateTimeUtc();

            await activityRepository.UpdateActivityAsync(activity);

            return activity;
        }

        public async Task<IEnumerable<Lap>> GetLapsAsync(ulong activityId)
        {
            var activities = await GetActivitiesAsync();
            return activities.FirstOrDefault(activity => activity.Id == activityId)?.Laps;
        }
    }
}