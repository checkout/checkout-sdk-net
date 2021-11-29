using System;
using Checkout.Common;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateTokenInstrumentResponse : CreateInstrumentResponse,
        IEquatable<CreateTokenInstrumentResponse>
    {
        public CreateTokenInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Scheme { get; set; }

        public string Last4 { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public CustomerResponse Customer { get; set; }

        public bool Equals(CreateTokenInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Fingerprint == other.Fingerprint && ExpiryMonth == other.ExpiryMonth &&
                   ExpiryYear == other.ExpiryYear && Scheme == other.Scheme && Last4 == other.Last4 &&
                   Bin == other.Bin && CardType == other.CardType && CardCategory == other.CardCategory &&
                   Issuer == other.Issuer && IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType && Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateTokenInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Fingerprint);
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(Scheme);
            hashCode.Add(Last4);
            hashCode.Add(Bin);
            hashCode.Add((int) CardType);
            hashCode.Add((int) CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add((int) IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            hashCode.Add(Customer);
            return hashCode.ToHashCode();
        }
    }
}