using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestSofortSource : AbstractRequestSource
    {
        public RequestSofortSource() : base(PaymentSourceType.Sofort)
        {
        }
    }
}