using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextDetailsResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the payment context.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        public PaymentContextDetailsStatusType? Status { get; set; }

        public PaymentContextsResponse PaymentRequest { get; set; }

        public PaymentContextsPartnerMetadata PartnerMetadata { get; set; }
    }
}