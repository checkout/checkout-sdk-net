using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Response
{
    public sealed class PaymentInstructionResponse : Resource, IEquatable<PaymentInstructionResponse>
    {
        public DateTime? ValueDate { get; set; }

        public bool Equals(PaymentInstructionResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ValueDate.Equals(other.ValueDate);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentInstructionResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ValueDate.GetHashCode();
        }
    }
}