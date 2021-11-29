using System;

namespace Checkout.Apm.Ideal
{
    public sealed class CuriesLink : IEquatable<CuriesLink>
    {
        public string Name { get; set; }

        public string Href { get; set; }

        public bool? Templated { get; set; }

        public bool Equals(CuriesLink other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Href == other.Href && Templated == other.Templated;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CuriesLink other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Href, Templated);
        }
    }
}