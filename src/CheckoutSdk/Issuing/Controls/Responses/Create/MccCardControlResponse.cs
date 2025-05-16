using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public class MccCardControlResponse : AbstractCardControlResponse
    {
        public MccCardControlResponse() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}