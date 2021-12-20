using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Events
{
    public class EventResponse : Resource
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Version { get; set; }

        public DateTime? CreatedOn { get; set; }

        public IDictionary<string, object> Data { get; set; }

        public IList<EventNotificationSummaryResponse> Notifications { get; set; }
    }
}