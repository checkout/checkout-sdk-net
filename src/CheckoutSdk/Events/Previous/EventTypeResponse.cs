using System.Collections.Generic;

namespace Checkout.Events.Previous
{
    public class EventTypesResponse
    {
        public string Version { get; set; }

        public IList<string> EventTypes { get; set; }
    }
}