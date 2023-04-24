namespace Checkout.Issuing.Controls.Requests.Create
{
    public class VelocityCardControlRequest : CardControlRequest
    {
        public VelocityCardControlRequest() : base(Controls.ControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}