using Checkout.Common;

namespace Checkout.Payments.Request
{
    public class PaymentCustomerRequest : CustomerRequest
    {
        public string Id { get; set; }
        public string TaxNumber { get; set; }
    }
}