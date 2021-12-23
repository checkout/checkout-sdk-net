using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Workflows.Four.Events
{
    public class SubjectEventsResponse
    {
        [JsonProperty(PropertyName = "data")] public IList<SubjectEvent> Events { get; set; }
    }
}