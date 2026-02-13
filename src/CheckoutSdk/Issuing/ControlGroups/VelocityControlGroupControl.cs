using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups
{
    public class VelocityControlGroupControl : ControlGroupControl
    {
        public VelocityControlGroupControl() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}