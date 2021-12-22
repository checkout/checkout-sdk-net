using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Workflows.Four
{
    public class GetWorkflowsResponse
    {
        [JsonProperty(PropertyName = "data")] public IList<Workflow> Workflows { get; set; }
    }
}