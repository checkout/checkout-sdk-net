using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Customers
{
    public sealed class CustomerDetailsResponse : IEquatable<CustomerDetailsResponse>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [JsonProperty(PropertyName = "default")]
        public string DefaultId { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public IList<string> Instruments { get; set; }

        public bool Equals(CustomerDetailsResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Email == other.Email && DefaultId == other.DefaultId && Name == other.Name &&
                   Equals(Phone, other.Phone) && Equals(Metadata, other.Metadata) &&
                   Equals(Instruments, other.Instruments);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CustomerDetailsResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, DefaultId, Name, Phone, Metadata, Instruments);
        }
    }
}