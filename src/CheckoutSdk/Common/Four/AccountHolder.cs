using System;

namespace Checkout.Common.Four
{
    public sealed class AccountHolder : IEquatable<AccountHolder>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(AccountHolder other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstName == other.FirstName && LastName == other.LastName &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AccountHolder) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, BillingAddress, Phone);
        }
    }
}