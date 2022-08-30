using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestMbwaySource : AbstractRequestSource
    {
        public RequestMbwaySource() : base(PaymentSourceType.Mbway)
        {
        }
    }
}