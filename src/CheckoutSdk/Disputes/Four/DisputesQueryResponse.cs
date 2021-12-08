using System;
using System.Collections.Generic;

namespace Checkout.Disputes.Four
{
    public sealed class DisputesQueryResponse : IEquatable<DisputesQueryResponse>
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Id { get; set; }

        public string EntityIds { get; set; }

        public string SubEntityIds { get; set; }

        public string Statuses { get; set; }

        public string PaymentId { get; set; }

        public string PaymentReference { get; set; }

        public string PaymentArn { get; set; }

        public string PaymentMcc { get; set; }

        public string ThisChannelOnly { get; set; }

        public int? TotalCount { get; set; }

        public IList<DisputeSummary> Data { get; set; }

        public bool Equals(DisputesQueryResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Limit == other.Limit && Skip == other.Skip && From == other.From && To == other.To &&
                   Id == other.Id && EntityIds == other.EntityIds && SubEntityIds == other.SubEntityIds &&
                   Statuses == other.Statuses && PaymentId == other.PaymentId &&
                   PaymentReference == other.PaymentReference && PaymentArn == other.PaymentArn &&
                   PaymentMcc == other.PaymentMcc && ThisChannelOnly == other.ThisChannelOnly &&
                   TotalCount == other.TotalCount && Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DisputesQueryResponse) obj);
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
            hashCode.Add(TotalCount);
            hashCode.Add(Data);
            return hashCode.ToHashCode();
        }
    }
}