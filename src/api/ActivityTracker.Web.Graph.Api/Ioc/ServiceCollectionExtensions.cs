using System;
using System.Collections.Generic;
using ActivityTracker.Application.Repositories;
using ActivityTracker.Application.Services;
using ActivityTracker.Data.Graph.Schema;
using ActivityTracker.Data.Persistence.Repositories;
using ActivityTracker.Data.Persistence.Repositories.Dtos;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
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
            services.AddSingleton<IActivityRepository>(new InMemoryActivityRepository(GetSeedData()));
        }

        public static void InstallGraph(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddSingleton(serviceProvider =>
                new ActivitiesSchema(new SelfActivatingServiceProvider(serviceProvider)));

            services.AddRouting();

            services
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = environment.IsDevelopment();
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx =>
                        logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                .AddDefaultEndpointSelectorPolicy()
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = environment.IsDevelopment())
                .AddWebSockets()
                .AddDataLoader();
        }

        private static SortedDictionary<ulong, ActivityDto> GetSeedData()
        {
            return new SortedDictionary<ulong, ActivityDto>
            {
                {
                    1,
                    new ActivityDto
                    {
                        Id = 1,
                        Name = "Test1",
                        Laps = new List<LapDto>
                        {
                            new LapDto
                            {
                                Id = 1, StartDateTimeUtc = DateTime.Now
                            }
                        }
                    }
                },
                {
                    2,
                    new ActivityDto
                    {
                        Id = 2,
                        Name = "Test2",
                        Laps = new List<LapDto>
                        {
                            new LapDto
                            {
                                Id = 1, StartDateTimeUtc = DateTime.Now
                            },
                            new LapDto
                            {
                                Id = 2, StartDateTimeUtc = DateTime.Now
                            }
                        }
                    }
                },
                {
                    3,
                    new ActivityDto
                    {
                        Id = 3,
                        Name = "Test3",
                        Laps = new List<LapDto>
                        {
                            new LapDto
                            {
                                Id = 1, StartDateTimeUtc = DateTime.Now
                            },
                            new LapDto
                            {
                                Id = 2, StartDateTimeUtc = DateTime.Now
                            },
                            new LapDto
                            {
                                Id = 3, StartDateTimeUtc = DateTime.Now
                            }
                        }
                    }
                }
            };
        }
    }
}
