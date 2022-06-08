using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestAlipayPlusHkSource : AbstractRequestSource
    {
        public RequestAlipayPlusHkSource() : base(PaymentSourceType.AlipayHk)
        {
        }
    }
}