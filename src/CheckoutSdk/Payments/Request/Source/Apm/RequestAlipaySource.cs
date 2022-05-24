using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAlipaySource : AbstractRequestSource
    {
        public RequestAlipaySource() : base(PaymentSourceType.Alipay)
        {
        }
    }
}