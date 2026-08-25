using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Vipps request source.
    /// </summary>
    public class RequestVippsSource : AbstractRequestSource
    {
        public RequestVippsSource() : base(PaymentSourceType.Vipps)
        {
        }
    }
}
