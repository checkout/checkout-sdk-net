using Checkout.Common;

namespace Checkout.Payments.Request
{
    public class PaymentCustomerRequest : CustomerRequest
    {
        public string TaxNumber { get; set; }
    }
}