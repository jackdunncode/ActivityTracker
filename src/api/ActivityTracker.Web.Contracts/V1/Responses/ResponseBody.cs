using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ActivityTracker.Web.Contracts.V1.Responses
{
    public class ResponseBody<T>
    {
        public ResponseBody()
        {
        }

        public ResponseBody(T result)
        {
            Result = result;
        }

        public ResponseBody(IEnumerable<ErrorResponseItem> errors)
        {
            Errors = errors;
        }

        public T Result { get; set; }

        [JsonPropertyName("errors")]
        public IEnumerable<ErrorResponseItem> Errors { get; set; }
    }
}
