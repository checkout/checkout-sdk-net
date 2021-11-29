using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Destination
{
    public sealed class PaymentRequestTokenDestination : PaymentRequestDestination,
        IEquatable<PaymentRequestTokenDestination>
    {
        public PaymentRequestTokenDestination() : base(PaymentDestinationType.Token)
        {
        }

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(PaymentRequestTokenDestination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && FirstName == other.FirstName && LastName == other.LastName &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentRequestTokenDestination other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, FirstName, LastName, BillingAddress, Phone);
        }
    }
}