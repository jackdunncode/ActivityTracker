using System.Collections.Generic;

namespace ActivityTracker.Application.Models
{
    public class Activity
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Lap> Laps { get; set; }
    }
}
