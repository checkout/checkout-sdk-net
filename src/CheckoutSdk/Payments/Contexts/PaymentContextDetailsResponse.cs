using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextDetailsResponse : HttpMetadata
    {
        public PaymentContextDetailsStatusType? Status { get; set; }
        
        public PaymentContextsResponse PaymentRequest { get; set; }

        public PaymentContextsPartnerMetadata PartnerMetadata { get; set; }
    }
}