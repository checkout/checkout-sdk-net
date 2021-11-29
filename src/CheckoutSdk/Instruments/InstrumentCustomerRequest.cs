using System;
using Checkout.Common;

namespace Checkout.Instruments
{
    public sealed class InstrumentCustomerRequest : CustomerRequest, IEquatable<InstrumentCustomerRequest>
    {
        public bool Default { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(InstrumentCustomerRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Default == other.Default && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is InstrumentCustomerRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Default, Phone);
        }
    }
}