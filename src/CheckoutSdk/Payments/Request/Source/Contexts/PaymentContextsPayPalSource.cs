using Checkout.Common;

namespace Checkout.Payments.Request.Source.Contexts
{
    public class PaymentContextsPayPalSource : AbstractRequestSource
    {
        public PaymentContextsPayPalSource() : base(PaymentSourceType.PayPal)
        {
        }
    }
}