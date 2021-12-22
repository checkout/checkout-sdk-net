using Newtonsoft.Json;

namespace Checkout.Workflows.Four.Events
{
    public class Event
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "display_name")] public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}