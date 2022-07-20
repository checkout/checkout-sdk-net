using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAlipayPlusGCashSource : AbstractRequestSource
    {
        public RequestAlipayPlusGCashSource() : base(PaymentSourceType.Gcash)
        {
        }
    }
}