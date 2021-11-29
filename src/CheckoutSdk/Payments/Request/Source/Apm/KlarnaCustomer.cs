using System;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class KlarnaCustomer : IEquatable<KlarnaCustomer>
    {
        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public bool Equals(KlarnaCustomer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DateOfBirth == other.DateOfBirth && Gender == other.Gender;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is KlarnaCustomer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateOfBirth, Gender);
        }
    }
}