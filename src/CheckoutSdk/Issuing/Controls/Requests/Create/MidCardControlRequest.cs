using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Create
{
    public class MidCardControlRequest : AbstractCardControlRequest
    {
        public MidCardControlRequest() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit MidLimit { get; set; }
    }
}