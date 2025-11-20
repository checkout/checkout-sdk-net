using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Customer
    {
        public CustomerEmail Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public CustomerDevice Device { get; set; }

        public PaymentSetupMerchantAccount MerchantAccount { get; set; }
    }

    public class CustomerEmail
    {
        public string Address { get; set; }

        public bool? Verified { get; set; }
    }

    public class CustomerDevice
    {
        public string Locale { get; set; }
    }
}