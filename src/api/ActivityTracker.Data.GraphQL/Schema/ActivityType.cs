using ActivityTracker.Application.Models;
using ActivityTracker.Application.Services;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Schema
{
    public sealed class ActivityType : ObjectGraphType<Activity>
    {
        public ActivityType(IActivityService activityService)
        {
            Field(activity => activity.Id);
            Field(activity => activity.Name);
            Field(activity => activity.Laps);

            Field<LapType>("laps", resolve: ctx => activityService.GetLapsAsync(ctx.Source.Id));
        }
    }
}
