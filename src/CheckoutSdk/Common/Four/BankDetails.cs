using System;

namespace Checkout.Common.Four
{
    public sealed class BankDetails : IEquatable<BankDetails>
    {
        public string Name { get; set; }

        public string Branch { get; set; }

        public Address Address { get; set; }

        public bool Equals(BankDetails other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Branch == other.Branch && Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is BankDetails other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Branch, Address);
        }
    }
}