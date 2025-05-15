using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public class VelocityCardControlResponse : AbstractCardControlResponse
    {
        public VelocityCardControlResponse() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}