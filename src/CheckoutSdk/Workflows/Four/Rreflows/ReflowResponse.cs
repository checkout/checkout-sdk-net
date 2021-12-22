using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Workflows.Four.Reflows
{
    public class ReflowResponse
    {
        [JsonProperty(PropertyName = "request_id")] public string RequestId { get; set; }

        [JsonProperty(PropertyName = "error_type")] public string ErrorType { get; set; }

        [JsonProperty(PropertyName = "error_codes")] public IList<string> ErrorCodes { get; set; }
    }
}