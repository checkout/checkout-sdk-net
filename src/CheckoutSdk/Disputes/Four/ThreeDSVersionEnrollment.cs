using System;
using Checkout.Common.Four;

namespace Checkout.Disputes.Four
{
    public sealed class ThreeDsVersionEnrollment : IEquatable<ThreeDsVersionEnrollment>
    {
        public string Version { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }

        public bool Equals(ThreeDsVersionEnrollment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Version == other.Version && Equals(Enrolled, other.Enrolled);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ThreeDsVersionEnrollment other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Version, Enrolled);
        }
    }
}