﻿using System;
using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class BillingInformation : IEquatable<BillingInformation>
    {
        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(BillingInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Address.Equals(other.Address) && Phone.Equals(other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is BillingInformation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, Phone);
        }
    }
}