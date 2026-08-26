using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// GCash request source.
    /// </summary>
    public class RequestGcashSource : AbstractRequestSource
    {
        public RequestGcashSource() : base(PaymentSourceType.Gcash)
        {
        }
    }
}
