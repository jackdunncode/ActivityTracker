using System.Linq;
using ActivityTracker.Application.Models;
using ActivityTracker.Data.Persistence.Repositories.Dtos;

namespace ActivityTracker.Data.Persistence.Extensions
{
    public static class ActivityExtensions
    {
        public static Activity ToActivity(this ActivityDto activityDto)
        {
            return new Activity
            {
                Id = activityDto.Id,
                Laps = activityDto.Laps.Select(laps => laps.ToLap()),
                Name = activityDto.Name
            };
        }

        public static ActivityDto ToActivityDto(this Activity activity)
        {
            return new ActivityDto
            {
                Id = activity.Id,
                Laps = activity.Laps.Select(laps => laps.ToLapDto()),
                Name = activity.Name
            };
        }
    }
}
