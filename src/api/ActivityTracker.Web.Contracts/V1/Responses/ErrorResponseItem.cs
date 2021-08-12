using System.Text.Json.Serialization;

namespace ActivityTracker.Web.Contracts.V1.Responses
{
    public class ErrorResponseItem
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}