using System;

namespace ActivityTracker.Application.Services
{
    public class DateTimeUtcNowProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}