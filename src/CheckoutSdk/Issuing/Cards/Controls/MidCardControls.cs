using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Controls
{
    public class MidCardControls : AbstractCardControls
    {
        public MidCardControls() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit MidLimit { get; set; }
    }
}