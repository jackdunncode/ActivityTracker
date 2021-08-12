using System;

namespace ActivityTracker.Web.Api.Services
{
    public interface IDateTimeProvider
    {
        public DateTime GetDateTimeUtc();
    }
}
