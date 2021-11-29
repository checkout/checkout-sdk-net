using System;
using Checkout.Common;

namespace Checkout.Disputes.Four
{
    public sealed class DisputeSummary : Resource, IEquatable<DisputeSummary>
    {
        public string Id { get; set; }

        public string EntityId { get; set; }

        public string SubEntityId { get; set; }

        public DisputeCategory? Category { get; set; }

        public DisputeStatus? Status { get; set; }

        public int? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public string PaymentId { get; set; }

        public string PaymentActionId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string PaymentMcc { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime? EvidenceRequiredBy { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public DateTime? LastUpdate { get; set; }

        public bool Equals(DisputeSummary other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && EntityId == other.EntityId && SubEntityId == other.SubEntityId &&
                   Category == other.Category && Status == other.Status && Amount == other.Amount &&
                   Currency == other.Currency && ReasonCode == other.ReasonCode && PaymentId == other.PaymentId &&
                   PaymentActionId == other.PaymentActionId && PaymentReference == other.PaymentReference &&
                   PaymentArn == other.PaymentArn && PaymentMcc == other.PaymentMcc &&
                   PaymentMethod == other.PaymentMethod &&
                   Nullable.Equals(EvidenceRequiredBy, other.EvidenceRequiredBy) &&
                   Nullable.Equals(ReceivedOn, other.ReceivedOn) && Nullable.Equals(LastUpdate, other.LastUpdate);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is DisputeSummary other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(EntityId);
            hashCode.Add(SubEntityId);
            hashCode.Add(Category);
            hashCode.Add(Status);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(ReasonCode);
            hashCode.Add(PaymentId);
            hashCode.Add(PaymentActionId);
            hashCode.Add(PaymentReference);
            hashCode.Add(PaymentArn);
            hashCode.Add(PaymentMcc);
            hashCode.Add(PaymentMethod);
            hashCode.Add(EvidenceRequiredBy);
            hashCode.Add(ReceivedOn);
            hashCode.Add(LastUpdate);
            return hashCode.ToHashCode();
        }
    }
}