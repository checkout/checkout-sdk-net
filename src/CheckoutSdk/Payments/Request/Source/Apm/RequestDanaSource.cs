using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// DANA request source.
    /// </summary>
    public class RequestDanaSource : AbstractRequestSource
    {
        public RequestDanaSource() : base(PaymentSourceType.Dana)
        {
        }
    }
}
