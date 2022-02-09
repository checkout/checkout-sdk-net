#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Workflows.Four
{
    public class GetWorkflowsResponse
    {
#if NET5_0_OR_GREATER
        [JsonPropertyName("data")]
#else
        [JsonProperty(PropertyName = "data")]
#endif
        public IList<Workflow> Workflows { get; set; }
    }
}