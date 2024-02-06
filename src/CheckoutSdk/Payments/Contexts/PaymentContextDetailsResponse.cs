using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextDetailsResponse : HttpMetadata
    {
        public PaymentContextsResponse PaymentRequest { get; set; }

        public PaymentContextsPartnerMetadata PartnerMetadata { get; set; }
    }
}