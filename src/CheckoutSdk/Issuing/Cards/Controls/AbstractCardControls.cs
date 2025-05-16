using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Controls
{
    public abstract class AbstractCardControls
    {
        public IssuingControlType? ControlType { get; set; }
        
        public string Id { get; set; }
        
        public string Description { get; set; }
        
        protected AbstractCardControls(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}