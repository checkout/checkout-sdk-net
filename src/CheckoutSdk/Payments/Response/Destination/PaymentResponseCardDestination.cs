using System;
using Checkout.Common;

namespace Checkout.Payments.Response.Destination
{
    public sealed class PaymentResponseCardDestination : AbstractPaymentResponseDestination,
        IPaymentResponseDestination, IEquatable<PaymentResponseCardDestination>
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Last4 { get; set; }

        public string Fingerprint { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public new PaymentDestinationType? Type()
        {
            return base.Type;
        }

        public bool Equals(PaymentResponseCardDestination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Last4 == other.Last4 && Fingerprint == other.Fingerprint && Bin == other.Bin &&
                   CardType == other.CardType && CardCategory == other.CardCategory && Issuer == other.Issuer &&
                   IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentResponseCardDestination other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(Name);
            hashCode.Add(Last4);
            hashCode.Add(Fingerprint);
            hashCode.Add(Bin);
            hashCode.Add((int) CardType);
            hashCode.Add((int) CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add((int) IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            return hashCode.ToHashCode();
        }
    }
}