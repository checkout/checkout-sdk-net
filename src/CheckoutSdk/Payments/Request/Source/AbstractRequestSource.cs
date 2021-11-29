using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public abstract class AbstractRequestSource
    {
        public PaymentSourceType? Type { get; set; }

        protected AbstractRequestSource(PaymentSourceType type)
        {
            Type = type;
        }
    }
}