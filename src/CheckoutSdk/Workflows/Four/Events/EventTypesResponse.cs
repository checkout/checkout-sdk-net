using System.Collections.Generic;

namespace Checkout.Workflows.Four.Events
{
    public class EventTypesResponse
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<Event> Events { get; set; }
    }
}