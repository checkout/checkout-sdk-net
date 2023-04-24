using Checkout.Issuing.Controls.Requests;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public class VelocityCardControlResponse : CardControlResponse
    {
        public VelocityCardControlResponse() : base(Controls.ControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}