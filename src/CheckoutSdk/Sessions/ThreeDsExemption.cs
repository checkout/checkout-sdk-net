using Checkout.Common;

namespace Checkout.Sessions
{
    public class ThreeDsExemption
    {
        public string Requested { get; set; }

        public Exemption? Applied { get; set; }

        public string Code { get; set; }
    }
}