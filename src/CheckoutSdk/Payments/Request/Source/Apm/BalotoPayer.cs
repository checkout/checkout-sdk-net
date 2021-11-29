using System;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class BalotoPayer : IEquatable<BalotoPayer>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool Equals(BalotoPayer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is BalotoPayer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email);
        }
    }
}