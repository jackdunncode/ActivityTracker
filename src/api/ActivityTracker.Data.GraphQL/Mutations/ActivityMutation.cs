using ActivityTracker.Application.Models;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Types;
using ActivityTracker.Web.Contracts.V1.Requests;
using GraphQL;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Mutations
{
    public class ActivityMutation : ObjectGraphType<object>
    {
        public ActivityMutation(IActivityService activityService)
        {
            Name = "Mutation";

            Field<ActivityType>(
                "createActivity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ActivityCreateInputType>>
                    {
                        Name = "createActivityRequest"
                    }),
                resolve: ctx =>
                {
                    var createActivityRequest = ctx.GetArgument<CreateActivityRequest>("createActivityRequest");
                    var activity = new Activity
                    {
                        Id = 10,
                        Name = createActivityRequest.Name
                    };
                    return activityService.CreateActivityAsync(activity, createActivityRequest.StartImmediately);
                });

            Field<ULongGraphType>(
                "deleteActivity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ULongGraphType>>
                    {
                        Name = "activityId"
                    }),
                resolve: ctx =>
                {
                    var activityId = ctx.GetArgument<ulong>("activityId");
                    return activityService.DeleteActivityAsync(activityId);
                });

            Field<ActivityType>(
                "startActivity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ULongGraphType>>
                    {
                        Name = "activityId"
                    }),
                resolve: ctx =>
                {
                    var activityId = ctx.GetArgument<ulong>("activityId");
                    return activityService.StartActivityAsync(activityId);
                });

            Field<ActivityType>(
                "stopActivity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ULongGraphType>>
                    {
                        Name = "activityId"
                    }),
                resolve: ctx =>
                {
                    var activityId = ctx.GetArgument<ulong>("activityId");
                    return activityService.StopActivityAsync(activityId);
                });
        }
    }
}
