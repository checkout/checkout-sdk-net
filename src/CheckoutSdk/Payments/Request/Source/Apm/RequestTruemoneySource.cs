using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// TrueMoney request source.
    /// </summary>
    public class RequestTruemoneySource : AbstractRequestSource
    {
        public RequestTruemoneySource() : base(PaymentSourceType.Truemoney)
        {
        }
    }
}
