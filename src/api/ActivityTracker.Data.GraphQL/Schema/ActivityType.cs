using ActivityTracker.Application.Models;
using ActivityTracker.Application.Services;
using GraphQL.MicrosoftDI;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Schema
{
    public class ActivityType : ObjectGraphType<Activity>
    {
        public ActivityType()
        {
            Field(activity => activity.Id);
            Field(activity => activity.Name);
            Field<ListGraphType<LapType>>()
                .Name("laps")
                .Resolve()
                .WithService<IActivityService>()
                .ResolveAsync(async (context, service) =>
                {
                    var laps = await service.GetLapsByActivityIdAsync(context.Source.Id);
                    return laps;
                });
        }
    }
}
