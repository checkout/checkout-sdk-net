using Checkout.Common;

namespace Checkout.Payments
{
    public sealed class ThreeDsEnrollment
    {
        public bool? Downgraded { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }
       
    }
}