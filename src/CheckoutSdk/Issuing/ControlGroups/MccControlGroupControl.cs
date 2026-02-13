using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups
{
    public class MccControlGroupControl : ControlGroupControl
    {
        public MccControlGroupControl() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}