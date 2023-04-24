namespace Checkout.Issuing.Controls.Requests.Create
{
    public abstract class CardControlRequest
    {
        public ControlType? ControlType { get; set; }

        public string Description { get; set; }

        public string TargetId { get; set; }

        protected CardControlRequest(ControlType type)
        {
            ControlType = type;
        }
    }
}