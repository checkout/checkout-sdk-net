using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Customer
    {
        /// <summary>
        /// The customer's email information
        /// </summary>
        public CustomerEmail Email { get; set; }

        /// <summary>
        /// &lt;= 100 characters
        /// The customer's full name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's phone number
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Information about the customer's device
        /// </summary>
        public CustomerDevice Device { get; set; }

        /// <summary>
        /// Details of the account the customer holds with the merchant
        /// </summary>
        public PaymentSetupMerchantAccount MerchantAccount { get; set; }
    }

    public class CustomerEmail
    {
        /// <summary>
        /// The customer's email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Whether the email address has been verified
        /// </summary>
        public bool? Verified { get; set; }
    }

    public class CustomerDevice
    {
        /// <summary>
        /// The locale setting of the customer's device (e.g., "en-US")
        /// </summary>
        public string Locale { get; set; }
    }
}