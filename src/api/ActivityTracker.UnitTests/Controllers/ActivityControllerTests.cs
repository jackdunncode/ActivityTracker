using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Web.Api.Controllers;
using ActivityTracker.Web.Api.Services;
using ActivityTracker.Web.Contracts.V1.Requests;
using ActivityTracker.Web.Contracts.V1.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ActivityTracker.UnitTests.Controllers
{
    public class ActivityControllerTests
    {
        private readonly Mock<IActivityService> activityServiceMock;
        private readonly ActivityController activityController;

        public ActivityControllerTests()
        {
            activityServiceMock = new Mock<IActivityService>();
            activityController = new ActivityController(activityServiceMock.Object);
        }

        public static IEnumerable<object[]> InvalidCreateRequestData =>
            new List<object[]>
            {
                new object[] { (CreateActivityRequest)null },
                new object[] { new CreateActivityRequest() },
                new object[] { new CreateActivityRequest { Name = string.Empty } },
                new object[] { new CreateActivityRequest { Name = " " } }
            };

        [Fact]
        public async Task CreateActivityAsync_ValidRequest_ReturnsOkWithACreateActivityResponse()
        {
            var createActivityRequest = new CreateActivityRequest
            {
                Name = "MyNewActivity",
                StartImmediately = false
            };

            var createActivityResponse = new CreateActivityResponse
            {
                Activity = new ActivityResponse
                {
                    Id = 1,
                    Name = createActivityRequest.Name,
                    Laps = Enumerable.Empty<LapResponse>()
                }
            };

            activityServiceMock.Setup(x => x.CreateActivityAsync(
                    It.Is<CreateActivityRequest>(req =>
                        string.Equals(req.Name, createActivityRequest.Name) &&
                        req.StartImmediately == createActivityRequest.StartImmediately)))
                .ReturnsAsync(createActivityResponse);

            var actionResult = await activityController.CreateActivityAsync(createActivityRequest);
            var objectResult = (ObjectResult)actionResult;

            objectResult.Should().BeOfType<OkObjectResult>();
            var responseBody = Assert.IsType<ResponseBody<CreateActivityResponse>>(objectResult.Value);
            responseBody.Errors.Should().BeNullOrEmpty();
            responseBody.Result.Activity.Id.Should().Be(createActivityResponse.Activity.Id);
            responseBody.Result.Activity.Name.Should().Be(createActivityResponse.Activity.Name);
            responseBody.Result.Activity.Laps.Should().BeEquivalentTo(createActivityResponse.Activity.Laps);
        }

        [Theory]
        [MemberData(nameof(InvalidCreateRequestData))]
        public async Task CreateActivityAsync_InvalidRequest_ReturnsBadRequestWithAnError(CreateActivityRequest createActivityRequest)
        {
            var actionResult = await activityController.CreateActivityAsync(createActivityRequest);

            var objectResult = (ObjectResult)actionResult;

            objectResult.Should().BeOfType<BadRequestObjectResult>();
            var responseBody = Assert.IsType<ResponseBody<object>>(objectResult.Value);
            responseBody.Errors.Should().HaveCount(1);
            responseBody.Errors.Should().Contain(item => string.Equals(item.Message, "Activity name is not specified"));
        }
    }
}
