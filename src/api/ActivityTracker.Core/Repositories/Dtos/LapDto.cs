using System;

namespace ActivityTracker.Core.Repositories.Dtos
{
    public class LapDto
    {
        public ulong Id { get; set; }

        public DateTime? StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }
    }
}
