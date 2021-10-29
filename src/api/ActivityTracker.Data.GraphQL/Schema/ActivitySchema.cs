using ActivityTracker.Data.Graph.Queries;

namespace ActivityTracker.Data.Graph.Schema
{
    public class ActivitySchema : GraphQL.Types.Schema
    {
        public ActivitySchema(ActivitiesQuery query)
        {
            Query = query;
        }
    }
}
