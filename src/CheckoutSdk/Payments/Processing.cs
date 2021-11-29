using System;

namespace Checkout.Payments
{
    public sealed class Processing : IEquatable<Processing>
    {
        public string AcquirerReferenceNumber { get; set; }

        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }

        public bool Equals(Processing other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AcquirerReferenceNumber == other.AcquirerReferenceNumber &&
                   RetrievalReferenceNumber == other.RetrievalReferenceNumber &&
                   AcquirerTransactionId == other.AcquirerTransactionId;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Processing other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AcquirerReferenceNumber, RetrievalReferenceNumber, AcquirerTransactionId);
        }
    }
}