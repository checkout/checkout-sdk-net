namespace Checkout.Issuing.Controls.Requests.Create
{
    public class MccCardControlRequest : CardControlRequest
    {
        public MccCardControlRequest() : base(Controls.ControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}