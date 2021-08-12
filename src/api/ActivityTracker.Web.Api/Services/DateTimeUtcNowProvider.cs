using System;

namespace ActivityTracker.Web.Api.Services
{
    public class DateTimeUtcNowProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}