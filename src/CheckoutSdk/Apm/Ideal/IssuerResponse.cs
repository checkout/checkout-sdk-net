using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Apm.Ideal
{
    public sealed class IssuerResponse : Resource, IEquatable<IssuerResponse>
    {
        public List<IdealCountry> Countries { get; set; }

        public bool Equals(IssuerResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Countries, other.Countries);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IssuerResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Countries != null ? Countries.GetHashCode() : 0);
        }
    }
}