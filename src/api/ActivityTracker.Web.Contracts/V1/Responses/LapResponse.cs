using System;

namespace ActivityTracker.Web.Contracts.V1.Responses
{
    public class LapResponse
    {
        public ulong Id { get; set; }
        public DateTime? StartDateTimeUtc { get; set; }
        public DateTime? EndDateTimeUtc { get; set; }
    }
}