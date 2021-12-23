using System.Collections.Generic;

namespace Checkout.Workflows.Four.Events
{
    public class GetEventResponse
    {
        public string Id { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public string Timestamp { get; set; }

        public string Version { get; set; }

        public IDictionary<string, object> Data { get; set; }
    }
}