using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPayPalSource : AbstractRequestSource
    {
        public RequestPayPalSource() : base(PaymentSourceType.PayPal)
        {
        }

        public BillingPlan plan { get; set; }
    }
}