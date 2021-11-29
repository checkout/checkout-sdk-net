using System;
using System.Collections.Generic;

namespace Checkout.Payments.Four
{
    public sealed class RefundRequest : IEquatable<RefundRequest>
    {
        public long? Amount { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public bool Equals(RefundRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Reference == other.Reference && Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RefundRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Reference, Metadata);
        }
    }
}