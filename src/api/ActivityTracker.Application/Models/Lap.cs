using System;

namespace ActivityTracker.Application.Models
{
    public class Lap
    {
        public ulong Id { get; set; }

        public DateTime? StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }
    }
}
