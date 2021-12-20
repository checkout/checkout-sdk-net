using System.Collections.Generic;

namespace Checkout.Events
{
    public class EventTypesResponse
    {
        public string Version { get; set; }

        public IList<string> EventTypes { get; set; }
    }
}