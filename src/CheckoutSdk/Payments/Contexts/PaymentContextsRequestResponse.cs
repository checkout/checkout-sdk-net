using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsRequestResponse : Resource
    {
        public string Id { get; set; }

        public PaymentContextsPartnerMetadata PartnerMetadata { get; set; }
    }
}