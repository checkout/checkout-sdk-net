using System;
using System.Collections.Generic;

namespace Checkout.Apm.Klarna
{
    public sealed class Klarna : IEquatable<Klarna>
    {
        public string Description { get; set; }

        public IList<KlarnaProduct> Products { get; set; }

        public IList<KlarnaShippingInfo> ShippingInfo { get; set; }

        public long? ShippingDelay { get; set; }

        public bool Equals(Klarna other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Description == other.Description && Equals(Products, other.Products) &&
                   Equals(ShippingInfo, other.ShippingInfo) && ShippingDelay == other.ShippingDelay;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Klarna other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description, Products, ShippingInfo, ShippingDelay);
        }
    }
}