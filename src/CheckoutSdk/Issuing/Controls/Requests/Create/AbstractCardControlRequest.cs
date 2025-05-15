using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Create
{
    public abstract class AbstractCardControlRequest
    {
        public IssuingControlType? ControlType { get; set; }

        public string Description { get; set; }

        public string TargetId { get; set; }

        protected AbstractCardControlRequest(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}