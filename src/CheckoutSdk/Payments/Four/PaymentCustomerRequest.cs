using Checkout.Common;

namespace Checkout.Payments.Four
{
    public class PaymentCustomerRequest : CustomerRequest
    {
        public string TaxNumber { get; set; }
    }
}