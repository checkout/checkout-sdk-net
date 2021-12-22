using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Events
{
    public sealed class EventNotificationResponse : Resource
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool? Success { get; set; }

        public string ContentType { get; set; }

        public IList<AttemptSummaryResponse> Attempts { get; set; }
    }
}