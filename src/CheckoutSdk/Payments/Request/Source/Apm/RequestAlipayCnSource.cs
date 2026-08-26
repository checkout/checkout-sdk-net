using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Alipay CN request source.
    /// </summary>
    public class RequestAlipayCnSource : AbstractRequestSource
    {
        public RequestAlipayCnSource() : base(PaymentSourceType.AlipayCn)
        {
        }
    }
}
