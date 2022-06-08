using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestAlipayPlusCnSource : AbstractRequestSource
    {
        public RequestAlipayPlusCnSource() : base(PaymentSourceType.AlipayCn)
        {
        }
    }
}