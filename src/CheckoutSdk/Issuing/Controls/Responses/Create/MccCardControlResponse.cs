using Checkout.Issuing.Controls.Requests;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public class MccCardControlResponse : CardControlResponse
    {
        public MccCardControlResponse() : base(Controls.ControlType.MccLimit)
        {
        }

        public MccLimit MccLimit { get; set; }
    }
}