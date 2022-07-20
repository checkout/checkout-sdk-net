using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAlipayPlusHkSource : AbstractRequestSource
    {
        public RequestAlipayPlusHkSource() : base(PaymentSourceType.AlipayHk)
        {
        }
    }
}