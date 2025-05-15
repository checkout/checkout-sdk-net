using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Create
{
    public class VelocityCardControlRequest : AbstractCardControlRequest
    {
        public VelocityCardControlRequest() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}