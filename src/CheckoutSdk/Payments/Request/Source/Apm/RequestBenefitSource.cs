using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBenefitSource : AbstractRequestSource
    {
        public RequestBenefitSource() : base(PaymentSourceType.Benefit)
        {
        }
    }
}