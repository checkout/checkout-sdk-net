using Checkout.Common;

namespace Checkout.Payments.Request.Source.Contexts
{
    public class PaymentContextsPaypalSource : AbstractRequestSource
    {
        public PaymentContextsPaypalSource() : base(PaymentSourceType.PayPal)
        {
        }
    }
}