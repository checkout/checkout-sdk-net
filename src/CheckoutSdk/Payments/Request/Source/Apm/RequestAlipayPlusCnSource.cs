using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAlipayPlusCnSource : AbstractRequestSource
    {
        public RequestAlipayPlusCnSource() : base(PaymentSourceType.AlipayCn)
        {
        }
    }
}