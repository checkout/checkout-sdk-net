using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    public sealed class CardTokenRequest : IEquatable<CardTokenRequest>
    {
        public readonly TokenType Type = TokenType.Card;

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(CardTokenRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Number == other.Number && ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear &&
                   Name == other.Name && Cvv == other.Cvv && Equals(BillingAddress, other.BillingAddress) &&
                   Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CardTokenRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, ExpiryMonth, ExpiryYear, Name, Cvv, BillingAddress, Phone);
        }
    }
}