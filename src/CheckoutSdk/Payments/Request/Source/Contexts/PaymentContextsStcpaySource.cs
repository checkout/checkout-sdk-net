using Checkout.Common;

namespace Checkout.Payments.Request.Source.Contexts
{
    public class PaymentContextsStcpaySource : AbstractRequestSource
    {
        public PaymentContextsStcpaySource() : base(PaymentSourceType.Stcpay)
        {
        }
    }
}