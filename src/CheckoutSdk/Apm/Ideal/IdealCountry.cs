using System;
using System.Collections.Generic;

namespace Checkout.Apm.Ideal
{
    public sealed class IdealCountry : IEquatable<IdealCountry>
    {
        public string Name { get; set; }

        public IList<Issuer> Issuers { get; set; }

        public bool Equals(IdealCountry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(Issuers, other.Issuers);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IdealCountry other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Issuers);
        }
    }
}