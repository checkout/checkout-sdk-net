using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBenefitPaySource : AbstractRequestSource
    {
        public string IntegrationType { get; set; }

        public RequestBenefitPaySource() : base(PaymentSourceType.BenefitPay)
        {
        }
    }
}