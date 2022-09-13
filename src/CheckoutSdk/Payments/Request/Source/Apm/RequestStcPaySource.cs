using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestStcPaySource : AbstractRequestSource
    {
        public RequestStcPaySource() : base(PaymentSourceType.Stcpay)
        {
        }
    }
}