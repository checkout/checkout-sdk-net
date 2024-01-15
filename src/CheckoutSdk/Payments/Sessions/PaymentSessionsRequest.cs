using Checkout.Common;

namespace Checkout.Payments.Sessions
{
    public class PaymentSessionsRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public Billing Billing { get; set; }

        public CustomerRequest Customer { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }
    }
}