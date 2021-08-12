using System.Collections.Generic;

namespace ActivityTracker.Web.Contracts.V1.Responses
{
    public class GetActivitiesResponse
    {
        public IEnumerable<ActivityResponse> Activities { get; set; }
    }
}