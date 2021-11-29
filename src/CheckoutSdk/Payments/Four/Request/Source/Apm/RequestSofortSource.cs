using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public sealed class RequestSofortSource : AbstractRequestSource
    {
        public RequestSofortSource() : base(PaymentSourceType.Sofort)
        {
        }
    }
}