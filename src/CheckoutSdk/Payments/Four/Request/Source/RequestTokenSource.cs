using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source
{
    public sealed class RequestTokenSource : AbstractRequestSource, IEquatable<RequestTokenSource>
    {
        public RequestTokenSource() : base(PaymentSourceType.Token)
        {
        }

        public string Token { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(RequestTokenSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestTokenSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, BillingAddress, Phone);
        }
    }
}