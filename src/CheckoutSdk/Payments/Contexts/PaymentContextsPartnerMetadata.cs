namespace Checkout.Payments.Contexts
{
    public class PaymentContextsPartnerMetadata
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string SessionId { get; set; }

        public string ClientToken { get; set; }
    }
}