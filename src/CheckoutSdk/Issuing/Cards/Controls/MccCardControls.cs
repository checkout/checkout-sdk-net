using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Controls
{
    public class MccCardControls : AbstractCardControls
    {
        public MccCardControls() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}