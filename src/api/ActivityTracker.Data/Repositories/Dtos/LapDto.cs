using System;

namespace ActivityTracker.Data.Repositories.Dtos
{
    public class LapDto
    {
        public ulong Id { get; set; }

        public DateTime? StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }
    }
}
