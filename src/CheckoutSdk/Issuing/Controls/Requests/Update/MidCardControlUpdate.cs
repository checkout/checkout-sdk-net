using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Update
{
    public class MidCardControlUpdate : AbstractCardControlUpdate
    {
        public MidCardControlUpdate() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit MidLimit { get; set; }
    }
}