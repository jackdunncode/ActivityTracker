using System;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Mutations;
using ActivityTracker.Data.Graph.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityTracker.Data.Graph.Schema
{
    public class ActivitiesSchema : GraphQL.Types.Schema
    {
        public ActivitiesSchema(
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = new ActivitiesQuery();
            Mutation = new ActivityMutation(serviceProvider.GetRequiredService<IActivityService>());
        }
    }
}
