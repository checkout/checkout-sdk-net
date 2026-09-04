using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// PayNow request source.
    /// </summary>
    public class RequestPayNowSource : AbstractRequestSource
    {
        public RequestPayNowSource() : base(PaymentSourceType.Paynow)
        {
        }
    }
}
