using System;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Core.Repositories;
using ActivityTracker.Core.Repositories.Dtos;
using ActivityTracker.Web.Api.Services;
using ActivityTracker.Web.Contracts.V1.Requests;
using FluentAssertions;
using Moq;
using Xunit;

namespace ActivityTracker.UnitTests.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task CreateActivityAsync_WithStartImmediatelySetToFalse_ReturnEmptyLaps()
        {
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var sut = new ActivityService(activityRepositoryMock.Object, dateTimeProviderMock.Object);

            var request = new CreateActivityRequest
            {
                Name = "TestActivity",
                StartImmediately = false
            };

            activityRepositoryMock
                .Setup(x => x.CreateActivityAsync(
                    It.Is<ActivityDto>(dto => string.Equals(dto.Name, request.Name))))
                .ReturnsAsync(new ActivityDto
                {
                    Id = 1,
                    Name = request.Name,
                    Laps = Enumerable.Empty<LapDto>()
                });

            var response = await sut.CreateActivityAsync(request);

            response.Activity.Name.Should().Be(request.Name);
            response.Activity.Laps.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateActivityAsync_WithStartImmediatelySetToTrue_ReturnOneLapWithNullEndDate()
        {
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var sut = new ActivityService(activityRepositoryMock.Object, dateTimeProviderMock.Object);

            var request = new CreateActivityRequest
            {
                Name = "TestActivity",
                StartImmediately = true
            };

            var lapDto = new LapDto
            {
                Id = 1,
                StartDateTimeUtc = DateTime.UtcNow
            };

            activityRepositoryMock
                .Setup(x => x.CreateActivityAsync(
                    It.Is<ActivityDto>(dto => string.Equals(dto.Name, request.Name))))
                .ReturnsAsync(new ActivityDto
                {
                    Id = 1,
                    Name = request.Name,
                    Laps = new LapDto[] { lapDto }
                });

            var response = await sut.CreateActivityAsync(request);

            response.Activity.Name.Should().Be(request.Name);
            response.Activity.Laps.Should().HaveCount(1);
            response.Activity.Laps.Should().OnlyContain(lapResponse => lapResponse.Id == lapDto.Id &&
                                                                       lapResponse.StartDateTimeUtc == lapDto.StartDateTimeUtc &&
                                                                       !lapResponse.EndDateTimeUtc.HasValue);
        }
    }
}