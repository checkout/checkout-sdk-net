using System;
using Newtonsoft.Json;

namespace Checkout.Disputes.Four
{
    public sealed class PaymentDispute : IEquatable<PaymentDispute>
    {
        public string Id { get; set; }

        public string ActionId { get; set; }

        public string ProcessingChannelId { get; set; }

        public int? Amount { get; set; }

        public string Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public string Mcc { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsVersionEnrollment ThreeDs { get; set; }

        public string Eci { get; set; }

        public bool? HasRefund { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public bool Equals(PaymentDispute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && ActionId == other.ActionId && ProcessingChannelId == other.ProcessingChannelId &&
                   Amount == other.Amount && Currency == other.Currency && Method == other.Method && Arn == other.Arn &&
                   Mcc == other.Mcc && Equals(ThreeDs, other.ThreeDs) && Eci == other.Eci &&
                   HasRefund == other.HasRefund && Nullable.Equals(ProcessedOn, other.ProcessedOn);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentDispute other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(ActionId);
            hashCode.Add(ProcessingChannelId);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Method);
            hashCode.Add(Arn);
            hashCode.Add(Mcc);
            hashCode.Add(ThreeDs);
            hashCode.Add(Eci);
            hashCode.Add(HasRefund);
            hashCode.Add(ProcessedOn);
            return hashCode.ToHashCode();
        }
    }
}