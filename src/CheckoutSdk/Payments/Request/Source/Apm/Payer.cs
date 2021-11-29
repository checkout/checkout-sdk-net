using System;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class Payer : IEquatable<Payer>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }

        public bool Equals(Payer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Email == other.Email && Document == other.Document;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Payer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Document);
        }
    }
}