using ActivityTracker.Application.Repositories;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityTracker.Web.Rest.Api.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeUtcNowProvider>();
            services.AddSingleton<IActivityService, ActivityService>();
            services.AddSingleton<IActivityRepository, InMemoryActivityRepository>();
        }
    }
}
