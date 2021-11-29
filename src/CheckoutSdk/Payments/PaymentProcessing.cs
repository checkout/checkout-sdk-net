using System;

namespace Checkout.Payments
{
    public sealed class PaymentProcessing : IEquatable<PaymentProcessing>
    {
        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }

        public string RecommendationCode { get; set; }

        public bool Equals(PaymentProcessing other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return RetrievalReferenceNumber == other.RetrievalReferenceNumber &&
                   AcquirerTransactionId == other.AcquirerTransactionId &&
                   RecommendationCode == other.RecommendationCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PaymentProcessing) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RetrievalReferenceNumber, AcquirerTransactionId, RecommendationCode);
        }
    }
}