using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups
{
    public class MidControlGroupControl : ControlGroupControl
    {
        public MidControlGroupControl() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit MidLimit { get; set; }
    }
}