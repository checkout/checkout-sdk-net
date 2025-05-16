using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class MccCardControlsResponse : AbstractCardControlsResponse
    {
        public MccCardControlsResponse() : base(IssuingControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}