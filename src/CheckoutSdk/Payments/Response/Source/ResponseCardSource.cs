using System;
using Checkout.Common;

namespace Checkout.Payments.Response.Source
{
    public sealed class ResponseCardSource : AbstractResponseSource, IResponseSource, IEquatable<ResponseCardSource>
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }

        public string Last4 { get; set; }

        public string Fingerprint { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string AvsCheck { get; set; }

        public string CvvCheck { get; set; }

        public bool? Payouts { get; set; }

        public string FastFunds { get; set; }

        public string PaymentAccountReference { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }

        public bool Equals(ResponseCardSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Scheme == other.Scheme && Last4 == other.Last4 && Fingerprint == other.Fingerprint &&
                   Bin == other.Bin && CardType == other.CardType && CardCategory == other.CardCategory &&
                   Issuer == other.Issuer && IssuerCountry == other.IssuerCountry && ProductId == other.ProductId &&
                   ProductType == other.ProductType && AvsCheck == other.AvsCheck && CvvCheck == other.CvvCheck &&
                   Payouts == other.Payouts && FastFunds == other.FastFunds &&
                   PaymentAccountReference == other.PaymentAccountReference;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ResponseCardSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(Name);
            hashCode.Add(Scheme);
            hashCode.Add(Last4);
            hashCode.Add(Fingerprint);
            hashCode.Add(Bin);
            hashCode.Add((int) CardType);
            hashCode.Add((int) CardCategory);
            hashCode.Add(Issuer);
            hashCode.Add((int) IssuerCountry);
            hashCode.Add(ProductId);
            hashCode.Add(ProductType);
            hashCode.Add(AvsCheck);
            hashCode.Add(CvvCheck);
            hashCode.Add(Payouts);
            hashCode.Add(FastFunds);
            hashCode.Add(PaymentAccountReference);
            return hashCode.ToHashCode();
        }
    }
}