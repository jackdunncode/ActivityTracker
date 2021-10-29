using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ActivityTracker.Data.Repositories;
using ActivityTracker.Data.Repositories.Dtos;
using ActivityTracker.FunctionalTests.Infrastructure;
using ActivityTracker.Web.Api;
using ActivityTracker.Web.Contracts.V1.Responses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestStack.BDDfy;
using Xunit;

namespace ActivityTracker.FunctionalTests.Features
{
    public class Tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpResponseMessage responseMessage;

        public Tests(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            HttpClient = factory.CreateClient();
        }

        private CustomWebApplicationFactory<Startup> Factory { get; }
        private HttpClient HttpClient { get; }

        [Fact]
        public void GivenActivitiesInDataStore_WhenGetRequestIsMade_ReturnActivitiesInResponse()
        {
            var activityDtos = new List<ActivityDto>
            {
                new ActivityDto
                {
                    Id = 1,
                    Name = "StartedActivity",
                    Laps = new List<LapDto>
                    {
                        new LapDto
                        {
                            Id = 1,
                            StartDateTimeUtc = DateTime.UtcNow
                        }
                    }
                },
                new ActivityDto
                {
                    Id = 2,
                    Name = "NotYetStartedActivity",
                    Laps = Enumerable.Empty<LapDto>()
                }
            };

            this.Given(s => SomeActivitiesExistsInTheDataStore(activityDtos))
                .When(s => AGetRequestForActivitiesIsMade())
                .Then(s => TheResponseShouldBe(HttpStatusCode.OK))
                .And(s => TheResponseShouldContainTheActivities(activityDtos))
                .BDDfy();
        }

        private async Task SomeActivitiesExistsInTheDataStore(IEnumerable<ActivityDto> activityDtos)
        {
            var repository = Factory.Services.GetRequiredService<IActivityRepository>();

            var createTasks = activityDtos.Select(activityDto => repository.CreateActivityAsync(activityDto));

            await Task.WhenAll(createTasks);
        }

        private async Task AGetRequestForActivitiesIsMade()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "activity");
            responseMessage = await HttpClient.SendAsync(request);
        }

        private void TheResponseShouldBe(HttpStatusCode statusCode)
        {
            responseMessage.StatusCode.Should().Be(statusCode);
        }

        private async Task TheResponseShouldContainTheActivities(IEnumerable<ActivityDto> activityDtos)
        {
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseBody<GetActivitiesResponse>>(stringResponse);

            response.Errors.Should().BeNullOrEmpty();
            response.Result.Activities.Should().HaveCount(activityDtos.Count());
        }
    }
}