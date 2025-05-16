using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public class MidCardControlResponse : AbstractCardControlResponse
    {
        public MidCardControlResponse() : base(IssuingControlType.MidLimit)
        {
        }

        public MidLimit VelocityLimit { get; set; }
    }
}