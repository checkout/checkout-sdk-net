using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Sender
{
    public sealed class PaymentIndividualSender : PaymentSender, IEquatable<PaymentIndividualSender>
    {
        public PaymentIndividualSender() : base(PaymentSenderType.Individual)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public bool Equals(PaymentIndividualSender other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstName == other.FirstName && LastName == other.LastName && Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentIndividualSender other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Address);
        }
    }
}