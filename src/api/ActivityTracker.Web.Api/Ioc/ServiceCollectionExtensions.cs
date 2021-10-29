using ActivityTracker.Data.Repositories;
using ActivityTracker.Web.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityTracker.Web.Api.Ioc
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
