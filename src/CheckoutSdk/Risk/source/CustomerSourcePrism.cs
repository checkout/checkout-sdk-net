using Checkout.Common;

namespace Checkout.Risk.source
{
    public sealed class CustomerSourcePrism : RiskPaymentRequestSource
    {
        public CustomerSourcePrism() : base(PaymentSourceType.Customer)
        {
        }

        public string Id { get; set; }
    }
}