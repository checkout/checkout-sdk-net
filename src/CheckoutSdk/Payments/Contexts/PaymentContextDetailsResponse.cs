using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextDetailsResponse : Resource
    {
        public PaymentContextsResponse PaymentRequest { get; set; }

        public PaymentContextsPartnerMetadata PartnerMetadata { get; set; }

        public object Customer { get; set; }
    }
}