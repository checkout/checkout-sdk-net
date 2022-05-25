using Checkout.Common.Four;

namespace Checkout.Disputes
{
    public class ThreeDsVersionEnrollment
    {
        public string Version { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }
    }
}