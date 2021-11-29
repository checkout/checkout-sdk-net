using System;
using Checkout.Common;

namespace Checkout.Instruments
{
    public sealed class InstrumentCustomerResponse : CustomerResponse, IEquatable<InstrumentCustomerResponse>
    {
        public bool Default { get; set; }

        public bool Equals(InstrumentCustomerResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Default == other.Default;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is InstrumentCustomerResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Default.GetHashCode();
        }
    }
}