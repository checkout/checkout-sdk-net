using Checkout.Common;

namespace Checkout.Risk.source
{
    public class CardSourcePrism : RiskPaymentRequestSource
    {
        public CardSourcePrism() : base(PaymentSourceType.Card)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
    }
}