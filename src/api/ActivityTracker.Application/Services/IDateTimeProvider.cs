using System;

namespace ActivityTracker.Application.Services
{
    public interface IDateTimeProvider
    {
        public DateTime GetDateTimeUtc();
    }
}
