using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class MidCardControlsResponse : AbstractCardControlsResponse
    {
        public MidCardControlsResponse() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit VelocityLimit { get; set; }
    }
}