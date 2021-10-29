using System;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Application.Models;
using ActivityTracker.Application.Repositories;
using ActivityTracker.Application.Services;
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

            const bool startImmediately = false;

            var request = new Activity
            {
                Name = "TestActivity"
            };

            activityRepositoryMock
                .Setup(x => x.CreateActivityAsync(
                    It.Is<Activity>(dto => string.Equals(dto.Name, request.Name))))
                .ReturnsAsync(new Activity
                {
                    Id = 1,
                    Name = request.Name,
                    Laps = Enumerable.Empty<Lap>()
                });

            var response = await sut.CreateActivityAsync(request, startImmediately);

            response.Name.Should().Be(request.Name);
            response.Laps.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateActivityAsync_WithStartImmediatelySetToTrue_ReturnOneLapWithNullEndDate()
        {
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var sut = new ActivityService(activityRepositoryMock.Object, dateTimeProviderMock.Object);
            
            const bool startImmediately = true;

            var request = new Activity
            {
                Name = "TestActivity"
            };

            var lapDto = new Lap
            {
                Id = 1,
                StartDateTimeUtc = DateTime.UtcNow
            };

            activityRepositoryMock
                .Setup(x => x.CreateActivityAsync(
                    It.Is<Activity>(dto => string.Equals(dto.Name, request.Name))))
                .ReturnsAsync(new Activity
                {
                    Id = 1,
                    Name = request.Name,
                    Laps = new Lap[] { lapDto }
                });

            var response = await sut.CreateActivityAsync(request, startImmediately);

            response.Name.Should().Be(request.Name);
            response.Laps.Should().HaveCount(1);
            response.Laps.Should().OnlyContain(lapResponse => lapResponse.Id == lapDto.Id &&
                                                                lapResponse.StartDateTimeUtc == lapDto.StartDateTimeUtc &&
                                                                !lapResponse.EndDateTimeUtc.HasValue);
        }
    }
}