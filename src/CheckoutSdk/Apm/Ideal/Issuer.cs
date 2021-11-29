using System;

namespace Checkout.Apm.Ideal
{
    public sealed class Issuer : IEquatable<Issuer>
    {
        public string Bic { get; set; }

        public string Name { get; set; }

        public bool Equals(Issuer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Bic == other.Bic && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Issuer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Bic, Name);
        }
    }
}