using Checkout.Common;

namespace Checkout.Payments
{
    public class ThreeDsEnrollment
    {
        public bool? Downgraded { get; set; }

        public ThreeDsEnrollmentStatus? Enrolled { get; set; }
        
        public string UpgradeReason { get; set; }
    }
}