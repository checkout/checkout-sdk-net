using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsCustomer
    {
        public PaymentSetupsCustomerEmail Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public PaymentSetupsCustomerDevice Device { get; set; }

        public PaymentSetupsCustomerMerchantAccount MerchantAccount { get; set; }
    }

    public class PaymentSetupsCustomerEmail
    {
        public string Address { get; set; }

        public bool? Verified { get; set; }
    }

    public class PaymentSetupsCustomerDevice
    {
        public string Locale { get; set; }
    }

    public class PaymentSetupsCustomerMerchantAccount
    {
        public string Id { get; set; }
    }
}