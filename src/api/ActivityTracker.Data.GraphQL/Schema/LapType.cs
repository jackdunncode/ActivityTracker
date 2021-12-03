using ActivityTracker.Application.Models;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Schema
{
    public class LapType : ObjectGraphType<Lap>
    {
        public LapType()
        {
            Field(lap => lap.Id);
            Field(lap => lap.StartDateTimeUtc, nullable: true);
            Field(lap => lap.EndDateTimeUtc, nullable: true);
        }
    }
}