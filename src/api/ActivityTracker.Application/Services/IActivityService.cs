﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;

namespace ActivityTracker.Application.Services
{
    public interface IActivityService
    {
        Task<Activity> CreateActivityAsync(Activity createActivityRequest, bool startImmediately);
        Task DeleteActivityAsync(ulong activityId);
        Task<IEnumerable<Activity>> GetActivitiesAsync();
        Task<Activity> StartActivityAsync(ulong activityId);
        Task<Activity> StopActivityAsync(ulong activityId);
        Task<IEnumerable<Lap>> GetLapsAsync(ulong activityId);
    }
}
