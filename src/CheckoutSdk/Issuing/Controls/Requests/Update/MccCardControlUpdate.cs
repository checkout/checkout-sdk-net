using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Update
{
    public class MccCardControlUpdate : AbstractCardControlUpdate
    {
        public MccCardControlUpdate() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}