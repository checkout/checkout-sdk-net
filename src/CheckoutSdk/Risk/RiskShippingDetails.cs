using System;
using Checkout.Common;

namespace Checkout.Risk
{
    public sealed class RiskShippingDetails : IEquatable<RiskShippingDetails>
    {
        public Address Address { get; set; }

        public bool Equals(RiskShippingDetails other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RiskShippingDetails other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Address != null ? Address.GetHashCode() : 0);
        }
    }
}