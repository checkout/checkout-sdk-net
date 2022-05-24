using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPoliSource : AbstractRequestSource
    {
        public RequestPoliSource() : base(PaymentSourceType.Poli)
        {
        }
    }
}