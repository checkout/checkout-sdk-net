using System;

namespace Checkout.Payments
{
    public sealed class RiskRequest : IEquatable<RiskRequest>
    {
        public bool? Enabled { get; set; }

        public bool Equals(RiskRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Enabled == other.Enabled;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RiskRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Enabled.GetHashCode();
        }
    }
}