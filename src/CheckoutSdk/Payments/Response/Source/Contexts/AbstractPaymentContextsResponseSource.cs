using Checkout.Common;

namespace Checkout.Payments.Response.Source.Contexts
{
    public abstract class AbstractPaymentContextsResponseSource
    {
        public PaymentSourceType? Type { get; set; }
        
        public AccountHolder AccountHolder { get; set; }
    }
}