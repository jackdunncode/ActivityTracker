using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Schema;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Queries
{
    public class ActivitiesQuery : ObjectGraphType<object>
    {
        public ActivitiesQuery(IActivityService activityService)
        {
            Name = "Query";
            Field<ListGraphType<ActivityType>>(
                "activities",
                resolve: ctx => activityService.GetActivitiesAsync());
        }
    }
}