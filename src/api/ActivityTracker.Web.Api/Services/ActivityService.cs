using System;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Data.Repositories;
using ActivityTracker.Data.Repositories.Dtos;
using ActivityTracker.Web.Contracts.V1.Requests;
using ActivityTracker.Web.Contracts.V1.Responses;

namespace ActivityTracker.Web.Api.Services
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

        public async Task<CreateActivityResponse> CreateActivityAsync(CreateActivityRequest createActivityRequest)
        {
            var laps = createActivityRequest.StartImmediately
                ? new LapDto[]
                {
                    new LapDto
                    {
                        Id = 1,
                        StartDateTimeUtc = dateTimeProvider.GetDateTimeUtc()
                    }
                }
                : Enumerable.Empty<LapDto>();

            var activityDto = new ActivityDto
            {
                Name = createActivityRequest.Name,
                Laps = laps
            };

            var result = await activityRepository.CreateActivityAsync(activityDto);

            return new CreateActivityResponse
            {
                Activity = ToActivityResponse(result)
            };
        }

        public Task DeleteActivityAsync(ulong activityId)
        {
            return activityRepository.DeleteActivityAsync(activityId);
        }

        public async Task<GetActivitiesResponse> GetActivitiesAsync()
        {
            var activityDtos = await activityRepository.GetActivitiesAsync();

            return new GetActivitiesResponse
            {
                Activities = activityDtos.Select(ToActivityResponse)
            };
        }

        public async Task<StartActivityResponse> StartActivityAsync(ulong activityId)
        {
            var activityDto = await activityRepository.GetActivityAsync(activityId);
            if (activityDto == null)
                return new StartActivityResponse();

            ulong newLapId = 1;

            var laps = activityDto.Laps.ToArray();
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

            var newLap = new LapDto
            {
                Id = newLapId,
                StartDateTimeUtc = dateTimeProvider.GetDateTimeUtc()
            };

            activityDto.Laps = activityDto.Laps.Concat(new[] {newLap});

            await activityRepository.UpdateActivityAsync(activityDto);

            return new StartActivityResponse
            {
                Activity = ToActivityResponse(activityDto)
            };
        }

        public async Task<StopActivityResponse> StopActivityAsync(ulong activityId)
        {
            var activityDto = await activityRepository.GetActivityAsync(activityId);
            if (activityDto == null)
                return new StopActivityResponse();

            var laps = activityDto.Laps.ToArray();
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

            await activityRepository.UpdateActivityAsync(activityDto);

            return new StopActivityResponse
            {
                Activity = ToActivityResponse(activityDto)
            };
        }

        private static ActivityResponse ToActivityResponse(ActivityDto activityDto) =>
            new ActivityResponse
            {
                Id = activityDto.Id,
                Name = activityDto.Name,
                Laps = activityDto.Laps.Select(lap => new LapResponse
                {
                    Id = lap.Id,
                    StartDateTimeUtc = lap.StartDateTimeUtc,
                    EndDateTimeUtc = lap.EndDateTimeUtc
                })
            };
    }
}