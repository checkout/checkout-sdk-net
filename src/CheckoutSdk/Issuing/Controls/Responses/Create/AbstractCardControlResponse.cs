using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    public abstract class AbstractCardControlResponse : HttpMetadata
    {
        public IssuingControlType? ControlType { get; set; }

        public string Id { get; set; }

        public string TargetId { get; set; }
        
        public bool? IsEditable { get; set; }
        
        public string CreatedDate { get; set; }

        public string LastModifiedDate { get; set; }
        
        public string Description { get; set; }

        protected AbstractCardControlResponse(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}