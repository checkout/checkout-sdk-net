using Checkout.Common;
using System;

namespace Checkout.Events.Previous
{
    public class EventSummaryResponse : Resource
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}