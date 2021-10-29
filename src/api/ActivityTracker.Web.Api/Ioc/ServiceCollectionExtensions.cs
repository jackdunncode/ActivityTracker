using ActivityTracker.Application.Repositories;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Queries;
using ActivityTracker.Data.Graph.Schema;
using ActivityTracker.Data.Persistence.Repositories;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityTracker.Web.Api.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeUtcNowProvider>();
            services.AddSingleton<IActivityService, ActivityService>();
        }

        public static void InstallPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IActivityRepository, InMemoryActivityRepository>();
        }

        public static void InstallGraph(this IServiceCollection services)
        {
            services
                .AddSingleton<ActivitySchema>()
                .AddSingleton<ActivitiesQuery>()
                .AddSingleton<ActivityType>()
                .AddSingleton<LapType>()
                .AddGraphQL(options => { })
                .AddSystemTextJson()
                .AddWebSockets()
                .AddGraphTypes(typeof(ActivitySchema));
        }
    }
}
