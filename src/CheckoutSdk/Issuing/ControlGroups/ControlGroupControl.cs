using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups
{
    public abstract class ControlGroupControl
    {
        public IssuingControlType? ControlType { get; set; }

        public string Description { get; set; }

        protected ControlGroupControl(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}