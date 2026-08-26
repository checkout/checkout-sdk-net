using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Alipay HK request source.
    /// </summary>
    public class RequestAlipayHkSource : AbstractRequestSource
    {
        public RequestAlipayHkSource() : base(PaymentSourceType.AlipayHk)
        {
        }
    }
}
