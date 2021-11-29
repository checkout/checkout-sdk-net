using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Sender
{
    public sealed class PaymentCorporateSender : PaymentSender, IEquatable<PaymentCorporateSender>
    {
        public PaymentCorporateSender() : base(PaymentSenderType.Corporate)
        {
        }

        public string CompanyName { get; set; }

        public Address Address { get; set; }

        public bool Equals(PaymentCorporateSender other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CompanyName == other.CompanyName && Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentCorporateSender other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompanyName, Address);
        }
    }
}