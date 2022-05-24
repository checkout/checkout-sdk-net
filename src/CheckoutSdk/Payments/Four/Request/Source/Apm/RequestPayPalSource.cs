using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestPayPalSource : AbstractRequestSource
    {
        public RequestPayPalSource() : base(PaymentSourceType.PayPal)
        {
        }
    }
}