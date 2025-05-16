using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class VelocityCardControlsResponse : AbstractCardControlsResponse
    {
        public VelocityCardControlsResponse() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}