namespace Checkout.Issuing.Controls.Requests.Update
{
    public class UpdateCardControlRequest
    {
        public string Description { get; set; }

        public VelocityLimit VelocityLimit { get; set; }

        public MccLimit MccLimit { get; set; }
    }
}