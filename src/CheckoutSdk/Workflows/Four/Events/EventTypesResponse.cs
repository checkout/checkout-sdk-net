using System.Collections.Generic;

namespace Checkout.Workflows.Four.Events
{
    public class EventTypesResponse : Event
    {
        public IList<Event> Events { get; set; }
    }
}