using System;

namespace Checkout.Common
{
    public sealed class Phone : IEquatable<Phone>
    {
        public string CountryCode { get; set; }

        public string Number { get; set; }

        public bool Equals(Phone other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CountryCode == other.CountryCode && Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Phone other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryCode, Number);
        }
    }
}