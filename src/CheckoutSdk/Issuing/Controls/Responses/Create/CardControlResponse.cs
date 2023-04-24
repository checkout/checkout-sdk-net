namespace Checkout.Issuing.Controls.Responses.Create
{
    public abstract class CardControlResponse : HttpMetadata
    {
        public ControlType? ControlType { get; set; }

        public string Id { get; set; }

        public string TargetId { get; set; }

        public string Description { get; set; }


        public string CreatedDate { get; set; }

        public string LastModifiedDate { get; set; }

        protected CardControlResponse(ControlType type)
        {
            ControlType = type;
        }
    }
}