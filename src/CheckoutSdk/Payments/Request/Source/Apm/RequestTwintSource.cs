using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Twint request source.
    /// </summary>
    public class RequestTwintSource : AbstractRequestSource
    {
        public RequestTwintSource() : base(PaymentSourceType.Twint)
        {
        }
    }
}
