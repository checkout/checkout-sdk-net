using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Disputes.Four
{
    public sealed class DisputeDetailsResponse : IEquatable<DisputeDetailsResponse>
    {
        public string Id { get; set; }

        public string EntityId { get; set; }

        public string SubEntityId { get; set; }

        public DisputeCategory? Category { get; set; }

        public int? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ReasonCode { get; set; }

        public DisputeStatus? Status { get; set; }

        public DisputeResolvedReason? ResolvedReason { get; set; }

        public IList<DisputeRelevantEvidence> RelevantEvidence { get; set; }

        public DateTime? EvidenceRequiredBy { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public DateTime? LastUpdate { get; set; }

        public PaymentDispute Payment { get; set; }

        public bool Equals(DisputeDetailsResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && EntityId == other.EntityId && SubEntityId == other.SubEntityId &&
                   Category == other.Category && Amount == other.Amount && Currency == other.Currency &&
                   ReasonCode == other.ReasonCode && Status == other.Status && ResolvedReason == other.ResolvedReason &&
                   Equals(RelevantEvidence, other.RelevantEvidence) &&
                   Nullable.Equals(EvidenceRequiredBy, other.EvidenceRequiredBy) &&
                   Nullable.Equals(ReceivedOn, other.ReceivedOn) && Nullable.Equals(LastUpdate, other.LastUpdate) &&
                   Equals(Payment, other.Payment);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is DisputeDetailsResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(EntityId);
            hashCode.Add(SubEntityId);
            hashCode.Add(Category);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(ReasonCode);
            hashCode.Add(Status);
            hashCode.Add(ResolvedReason);
            hashCode.Add(RelevantEvidence);
            hashCode.Add(EvidenceRequiredBy);
            hashCode.Add(ReceivedOn);
            hashCode.Add(LastUpdate);
            hashCode.Add(Payment);
            return hashCode.ToHashCode();
        }
    }
}