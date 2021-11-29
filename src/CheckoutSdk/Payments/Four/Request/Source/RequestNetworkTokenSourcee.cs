using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source
{
    public sealed class RequestNetworkTokenSource : AbstractRequestSource, IEquatable<RequestNetworkTokenSource>
    {
        public RequestNetworkTokenSource() : base(PaymentSourceType.NetworkToken)
        {
        }

        public string Token { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public NetworkTokenType? TokenType { get; set; }

        public string Cryptogram { get; set; }

        public string Eci { get; set; }

        public bool? Stored { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(RequestNetworkTokenSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear &&
                   TokenType == other.TokenType && Cryptogram == other.Cryptogram && Eci == other.Eci &&
                   Stored == other.Stored && Name == other.Name && Cvv == other.Cvv &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestNetworkTokenSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Token);
            hashCode.Add(ExpiryMonth);
            hashCode.Add(ExpiryYear);
            hashCode.Add(TokenType);
            hashCode.Add(Cryptogram);
            hashCode.Add(Eci);
            hashCode.Add(Stored);
            hashCode.Add(Name);
            hashCode.Add(Cvv);
            hashCode.Add(BillingAddress);
            hashCode.Add(Phone);
            return hashCode.ToHashCode();
        }
    }
}