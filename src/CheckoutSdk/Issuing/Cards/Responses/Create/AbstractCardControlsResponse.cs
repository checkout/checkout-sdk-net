using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public abstract class AbstractCardControlsResponse
    {
        public IssuingControlType? ControlType { get; set; }

        public string Id { get; set; }

        public string TargetId { get; set; }

        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedDate { get; set; }

        protected AbstractCardControlsResponse(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}