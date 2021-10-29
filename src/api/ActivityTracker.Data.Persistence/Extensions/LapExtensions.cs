using ActivityTracker.Application.Models;
using ActivityTracker.Data.Persistence.Repositories.Dtos;

namespace ActivityTracker.Data.Persistence.Extensions
{
    public static class LapExtensions
    {
        public static Lap ToLap(this LapDto lapDto)
        {
            return new Lap
            {
                Id = lapDto.Id,
                StartDateTimeUtc = lapDto.StartDateTimeUtc,
                EndDateTimeUtc = lapDto.EndDateTimeUtc
            };
        }

        public static LapDto ToLapDto(this Lap lapDto)
        {
            return new LapDto
            {
                Id = lapDto.Id,
                StartDateTimeUtc = lapDto.StartDateTimeUtc,
                EndDateTimeUtc = lapDto.EndDateTimeUtc
            };
        }
    }
}
