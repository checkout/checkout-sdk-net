using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Create
{
    public class MccCardControlRequest : AbstractCardControlRequest
    {
        public MccCardControlRequest() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}