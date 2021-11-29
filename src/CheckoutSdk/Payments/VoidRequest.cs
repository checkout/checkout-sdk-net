using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public sealed class VoidRequest : IEquatable<VoidRequest>
    {
        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public bool Equals(VoidRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Reference == other.Reference && Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is VoidRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Reference, Metadata);
        }
    }
}