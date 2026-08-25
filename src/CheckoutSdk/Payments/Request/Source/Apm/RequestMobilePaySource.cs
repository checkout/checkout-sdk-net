using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// MobilePay request source.
    /// </summary>
    public class RequestMobilePaySource : AbstractRequestSource
    {
        public RequestMobilePaySource() : base(PaymentSourceType.Mobilepay)
        {
        }
    }
}
