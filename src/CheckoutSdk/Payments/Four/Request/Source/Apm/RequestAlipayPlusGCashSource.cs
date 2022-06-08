using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestAlipayPlusGCashSource : AbstractRequestSource
    {
        public RequestAlipayPlusGCashSource() : base(PaymentSourceType.Gcash)
        {
        }
    }
}