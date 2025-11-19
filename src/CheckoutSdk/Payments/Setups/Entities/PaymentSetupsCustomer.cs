using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsCustomer
    {
        public PaymentSetupsCustomerEmail Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public PaymentSetupsCustomerDevice Device { get; set; }

        public PaymentSetupMerchantAccount MerchantAccount { get; set; }
    }
}