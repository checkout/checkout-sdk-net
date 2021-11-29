using Checkout.Common;

namespace Checkout.Risk.source
{
    public abstract class RiskPaymentRequestSource
    {
        protected RiskPaymentRequestSource(PaymentSourceType type)
        {
            Type = type;
        }

        public PaymentSourceType? Type { get; }
    }
}