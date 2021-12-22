using System;

namespace Checkout.Events
{
    public sealed class RetrieveEventsRequest
    {
        public string PaymentId { get; set; }

        public string ChargeId { get; set; }

        public string TrackId { get; set; }

        public string Reference { get; set; }

        public int? Skip { get; set; }

        public int? Limit { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}