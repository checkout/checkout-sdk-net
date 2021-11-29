using System;

namespace Checkout.Payments.Four
{
    public sealed class BillingDescriptor : IEquatable<BillingDescriptor>
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Reference { get; set; }

        public bool Equals(BillingDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && City == other.City && Reference == other.Reference;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is BillingDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, City, Reference);
        }
    }
}