using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestGiropaySource : AbstractRequestSource
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }
    }
}