﻿using Checkout.Common;
using System;

namespace Checkout.Payments
{
    public class BillingInformation : IEquatable<BillingInformation>
    {
        public Address Address { get; set; }
        public Phone Phone { get; set; }

        public bool Equals(BillingInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Address.Equals(Address) && other.Phone.Equals(Phone);
        }
    }
}
