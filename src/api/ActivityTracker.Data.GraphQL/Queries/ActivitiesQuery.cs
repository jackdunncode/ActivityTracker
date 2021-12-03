using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Schema;
using ActivityTracker.Data.Graph.Types;
using GraphQL.MicrosoftDI;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Queries
{
    public sealed class ActivitiesQuery : ObjectGraphType<object>
    {
        public ActivitiesQuery()
        {
            Name = "Query";
            Field<ListGraphType<ActivityType>>()
                .Name("activities")
                .Resolve()
                .WithService<IActivityService>()
                .ResolveAsync(async (_, service) =>
                {
                    var activities = await service.GetActivitiesAsync();
                    return activities;
                });
        }
    }
}