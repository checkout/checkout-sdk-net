using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Workflows
{
    public class GetWorkflowsResponse : HttpMetadata
    {
        [JsonProperty(PropertyName = "data")] public IList<Workflow> Workflows { get; set; }
    }
}