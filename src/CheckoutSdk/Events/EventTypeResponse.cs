using System.Collections.Generic;

namespace Checkout.Events
{
    public sealed class EventTypesResponse
    {
        public string Version { get; set; }

        public IList<string> EventTypes { get; set; }
    }
}