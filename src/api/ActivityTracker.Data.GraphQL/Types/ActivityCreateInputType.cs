using ActivityTracker.Web.Contracts.V1.Requests;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Types
{
    public sealed class ActivityCreateInputType : InputObjectGraphType<CreateActivityRequest>
    {
        public ActivityCreateInputType()
        {
            Name = "CreateActivityRequest";
            Field(createActivityRequest => createActivityRequest.Name);
            Field(createActivityRequest => createActivityRequest.StartImmediately);
        }
    }
}
