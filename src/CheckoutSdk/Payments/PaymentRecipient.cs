using System;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public sealed class PaymentRecipient : IEquatable<PaymentRecipient>
    {
        [JsonProperty("dob")] public string DateOfBirth { get; set; }

        public string AccountNumber { get; set; }

        public string Zip { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CountryCode? Country { get; set; }

        public bool Equals(PaymentRecipient other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DateOfBirth == other.DateOfBirth && AccountNumber == other.AccountNumber && Zip == other.Zip &&
                   FirstName == other.FirstName && LastName == other.LastName && Country == other.Country;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentRecipient other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateOfBirth, AccountNumber, Zip, FirstName, LastName, Country);
        }
    }
}