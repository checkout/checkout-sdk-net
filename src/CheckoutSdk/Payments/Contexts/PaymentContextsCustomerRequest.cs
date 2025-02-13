using Checkout.Common;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsCustomerRequest : CustomerRequest
    {
        public bool? EmailVerified { get; set; }
        
        public PaymentContextsCustomerSummary Summary { get; set; }
    }
}