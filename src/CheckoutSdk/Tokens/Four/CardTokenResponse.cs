using System;

namespace Checkout.Tokens.Four
{
    public sealed class CardTokenResponse : TokenResponse, IEquatable<CardTokenResponse>
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }

        public string Last4 { get; set; }

        public string Bin { get; set; }

        public string CardType { get; set; }

        public string CardCategory { get; set; }

        public string Issuer { get; set; }

        public string IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public bool Equals(CardTokenResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Scheme == other.Scheme && Last4 == other.Last4 && Bin == other.Bin && CardType == other.CardType &&
                   CardCategory == other.CardCategory && Issuer == other.Issuer &&
                   IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType;
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
            hashCode.Add(Name);
            hashCode.Add(Scheme);
            hashCode.Add(Last4);
            hashCode.Add(Bin);
            hashCode.Add(CardType);
            hashCode.Add(CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add(IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            return hashCode.ToHashCode();
        }
    }
}