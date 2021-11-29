using System;

namespace Checkout.Events
{
    public sealed class RetrieveEventsRequest : IEquatable<RetrieveEventsRequest>
    {
        public string PaymentId { get; set; }

        public string ChargeId { get; set; }

        public string TrackId { get; set; }

        public string Reference { get; set; }

        public int? Skip { get; set; }

        public int? Limit { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public bool Equals(RetrieveEventsRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PaymentId == other.PaymentId && ChargeId == other.ChargeId && TrackId == other.TrackId &&
                   Reference == other.Reference && Skip == other.Skip && Limit == other.Limit &&
                   Nullable.Equals(From, other.From) && Nullable.Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RetrieveEventsRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PaymentId, ChargeId, TrackId, Reference, Skip, Limit, From, To);
        }
    }
}