using Checkout.Common;

namespace Checkout.Risk.source
{
    public class IdSourcePrism : RiskPaymentRequestSource
    {
        public IdSourcePrism() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }
    }
}