using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Customers
{
    public sealed class CustomerRequest : IEquatable<CustomerRequest>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public bool Equals(CustomerRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Email == other.Email && Name == other.Name && Equals(Phone, other.Phone) &&
                   Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerRequest) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Name, Phone, Metadata);
        }
    }
}