using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    public sealed class CardTokenResponse : TokenResponse, IEquatable<CardTokenResponse>
    {
        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public string Name { get; set; }

        public bool Equals(CardTokenResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Scheme == other.Scheme && Last4 == other.Last4 && Bin == other.Bin && CardType == other.CardType &&
                   CardCategory == other.CardCategory && Issuer == other.Issuer &&
                   IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType && TokenFormat == other.TokenFormat &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CardTokenResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(Scheme);
            hashCode.Add(Last4);
            hashCode.Add(Bin);
            hashCode.Add(CardType);
            hashCode.Add(CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add(IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            hashCode.Add(TokenFormat);
            hashCode.Add(BillingAddress);
            hashCode.Add(Phone);
            hashCode.Add(Name);
            return hashCode.ToHashCode();
        }
    }
}