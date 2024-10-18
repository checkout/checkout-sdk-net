using Checkout.Common;

namespace Checkout.Payments.Request.Source.Contexts
{
    public class PaymentContextsTabbySource : AbstractRequestSource
    {
        public PaymentContextsTabbySource() : base(PaymentSourceType.Tabby)
        {
        }
    }
}