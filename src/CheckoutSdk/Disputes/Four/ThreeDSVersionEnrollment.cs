using Checkout.Common.Four;

namespace Checkout.Disputes.Four
{
    public sealed class ThreeDsVersionEnrollment
    {
        public string Version { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }
    }
}