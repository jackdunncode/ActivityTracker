using System;
using ActivityTracker.Application.Repositories;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Queries;
using ActivityTracker.Data.Graph.Schema;
using ActivityTracker.Data.Persistence.Repositories;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ActivityTracker.Web.Graph.Api.Ioc
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

        public static void InstallGraph(this IServiceCollection services, IWebHostEnvironment environment)
        {
            //services.AddSingleton<LapType>();
            //services.AddSingleton<ActivityType>();
            //services.AddSingleton<ActivitiesQuery>();
            //services.AddSingleton<ActivitiesSchema>();

            services.AddSingleton<ActivitiesSchema>(serviceProvider => new ActivitiesSchema(new SelfActivatingServiceProvider(serviceProvider)));

            services.AddRouting();

            services
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = environment.IsDevelopment();
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                .AddDefaultEndpointSelectorPolicy()
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = environment.IsDevelopment())
                .AddWebSockets()
                .AddDataLoader();
        }
    }
}
