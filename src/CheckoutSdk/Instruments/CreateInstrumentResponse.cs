using System;
using Checkout.Common;

namespace Checkout.Instruments
{
    public sealed class CreateInstrumentResponse : IEquatable<CreateInstrumentResponse>
    {
        public string Id { get; set; }

        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Last4 { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public InstrumentCustomerResponse Customer { get; set; }

        public bool Equals(CreateInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Type == other.Type && Fingerprint == other.Fingerprint &&
                   ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Last4 == other.Last4 && Bin == other.Bin && CardType == other.CardType &&
                   CardCategory == other.CardCategory && Issuer == other.Issuer &&
                   IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType && Equals(AccountHolder, other.AccountHolder) &&
                   Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add((int) Type);
            hashCode.Add(Fingerprint);
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(Name);
            hashCode.Add(Last4);
            hashCode.Add(Bin);
            hashCode.Add((int) CardType);
            hashCode.Add((int) CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add((int) IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            hashCode.Add(AccountHolder);
            hashCode.Add(Customer);
            return hashCode.ToHashCode();
        }
    }
}