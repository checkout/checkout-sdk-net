using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Touch 'n Go request source.
    /// </summary>
    public class RequestTngSource : AbstractRequestSource
    {
        public RequestTngSource() : base(PaymentSourceType.Tng)
        {
        }
    }
}
