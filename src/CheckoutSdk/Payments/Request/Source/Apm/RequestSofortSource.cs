using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestSofortSource : AbstractRequestSource
    {
        public RequestSofortSource() : base(PaymentSourceType.Sofort)
        {
        }
    }
}