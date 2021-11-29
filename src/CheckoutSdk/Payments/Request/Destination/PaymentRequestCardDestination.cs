using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Destination
{
    public sealed class PaymentRequestCardDestination : PaymentRequestDestination,
        IEquatable<PaymentRequestCardDestination>
    {
        public PaymentRequestCardDestination() : base(PaymentDestinationType.Card)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(PaymentRequestCardDestination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Number == other.Number && ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear &&
                   FirstName == other.FirstName && LastName == other.LastName && Name == other.Name &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentRequestCardDestination other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, ExpiryMonth, ExpiryYear, FirstName, LastName, Name, BillingAddress, Phone);
        }
    }
}