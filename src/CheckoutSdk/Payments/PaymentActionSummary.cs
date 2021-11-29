using System;

namespace Checkout.Payments
{
    public sealed class PaymentActionSummary : IEquatable<PaymentActionSummary>
    {
        public string Id { get; set; }

        public ActionType? Type { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public bool Equals(PaymentActionSummary other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Type == other.Type && ResponseCode == other.ResponseCode &&
                   ResponseSummary == other.ResponseSummary;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentActionSummary other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, (int) Type, ResponseCode, ResponseSummary);
        }
    }
}