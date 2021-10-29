using System.Collections.Generic;

namespace ActivityTracker.Data.Persistence.Repositories.Dtos
{
    public class ActivityDto
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<LapDto> Laps { get; set; }
    }
}
