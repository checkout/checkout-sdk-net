using System;

namespace Checkout.Payments.Four.Request
{
    public sealed class PayoutBillingDescriptor : IEquatable<PayoutBillingDescriptor>
    {
        public string Reference { get; set; }

        public bool Equals(PayoutBillingDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Reference == other.Reference;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PayoutBillingDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Reference != null ? Reference.GetHashCode() : 0);
        }
    }
}