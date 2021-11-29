using System;
using Checkout.Common;

namespace Checkout.Instruments
{
    public sealed class InstrumentAccountHolder : IEquatable<InstrumentAccountHolder>
    {
        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(InstrumentAccountHolder other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is InstrumentAccountHolder other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BillingAddress, Phone);
        }
    }
}