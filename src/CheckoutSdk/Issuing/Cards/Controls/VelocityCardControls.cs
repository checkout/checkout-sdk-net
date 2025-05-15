using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Controls
{
    public class VelocityCardControls : AbstractCardControls
    {
        public VelocityCardControls() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}