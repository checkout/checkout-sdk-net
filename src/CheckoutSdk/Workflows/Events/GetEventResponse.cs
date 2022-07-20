using System.Collections.Generic;

namespace Checkout.Workflows.Events
{
    public class GetEventResponse : HttpMetadata
    {
        public string Id { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public string Timestamp { get; set; }

        public string Version { get; set; }

        public IDictionary<string, object> Data { get; set; }
    }
}