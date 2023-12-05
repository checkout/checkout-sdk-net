using Checkout.Payments.Request.Source;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsRequest : PaymentContexts
    {
        public AbstractRequestSource Source { get; set; }
    }
}