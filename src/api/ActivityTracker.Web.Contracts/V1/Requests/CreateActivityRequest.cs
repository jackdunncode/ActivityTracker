namespace ActivityTracker.Web.Contracts.V1.Requests
{
    public class CreateActivityRequest
    {
        public string Name { get; set; }
        public bool StartImmediately { get; set; }
    }
}
