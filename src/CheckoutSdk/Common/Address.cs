using System;

namespace Checkout.Common
{
    public sealed class Address : IEquatable<Address>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public CountryCode? Country { get; set; }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AddressLine1 == other.AddressLine1 && AddressLine2 == other.AddressLine2 && City == other.City &&
                   State == other.State && Zip == other.Zip && Country == other.Country;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AddressLine1, AddressLine2, City, State, Zip, (int) Country);
        }
    }
}