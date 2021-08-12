using System.Collections.Generic;

namespace ActivityTracker.Web.Contracts.V1.Responses
{
    public class ActivityResponse
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<LapResponse> Laps { get; set; }
    }
}
