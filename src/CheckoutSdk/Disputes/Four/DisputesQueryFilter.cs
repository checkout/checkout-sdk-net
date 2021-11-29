using System;

namespace Checkout.Disputes.Four
{
    public sealed class DisputesQueryFilter : IEquatable<DisputesQueryFilter>
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Id { get; set; }

        public string EntityIds { get; set; }

        public string SubEntityIds { get; set; }

        public string Statuses { get; set; }

        public string PaymentId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string PaymentMcc { get; set; }

        public string ThisChannelOnly { get; set; }

        public bool Equals(DisputesQueryFilter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Limit == other.Limit && Skip == other.Skip && Nullable.Equals(From, other.From) &&
                   Nullable.Equals(To, other.To) && Id == other.Id && EntityIds == other.EntityIds &&
                   SubEntityIds == other.SubEntityIds && Statuses == other.Statuses && PaymentId == other.PaymentId &&
                   PaymentReference == other.PaymentReference && PaymentArn == other.PaymentArn &&
                   PaymentMcc == other.PaymentMcc && ThisChannelOnly == other.ThisChannelOnly;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is DisputesQueryFilter other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Limit);
            hashCode.Add(Skip);
            hashCode.Add(From);
            hashCode.Add(To);
            hashCode.Add(Id);
            hashCode.Add(EntityIds);
            hashCode.Add(SubEntityIds);
            hashCode.Add(Statuses);
            hashCode.Add(PaymentId);
            hashCode.Add(PaymentReference);
            hashCode.Add(PaymentArn);
            hashCode.Add(PaymentMcc);
            hashCode.Add(ThisChannelOnly);
            return hashCode.ToHashCode();
        }
    }
}