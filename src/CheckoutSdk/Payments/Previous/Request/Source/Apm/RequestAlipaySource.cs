using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestAlipaySource : AbstractRequestSource
    {
        public RequestAlipaySource() : base(PaymentSourceType.Alipay)
        {
        }
    }
}