using System;
using System.Collections.Generic;

namespace Checkout.Payments.Four
{
    public sealed class CaptureRequest : IEquatable<CaptureRequest>
    {
        public long? Amount { get; set; }

        public CaptureType? CaptureType { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public bool Equals(CaptureRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && CaptureType == other.CaptureType && Reference == other.Reference &&
                   Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CaptureRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, (int) CaptureType, Reference, Metadata);
        }
    }
}