using System;
using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class ThreeDsEnrollment : IEquatable<ThreeDsEnrollment>
    {
        public bool? Downgraded { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }

        public bool Equals(ThreeDsEnrollment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Downgraded == other.Downgraded && Enrolled == other.Enrolled;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ThreeDsEnrollment other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Downgraded, Enrolled);
        }
    }
}