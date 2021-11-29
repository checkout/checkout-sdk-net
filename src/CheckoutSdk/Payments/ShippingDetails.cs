using System;
using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class ShippingDetails : IEquatable<ShippingDetails>
    {
        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(ShippingDetails other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Address, other.Address) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ShippingDetails other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, Phone);
        }
    }
}