using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Customer
    {
        /// <summary>
        /// Details of the customer's email
        /// </summary>
        public CustomerEmail Email { get; set; }

        /// <summary>
        /// The customer's full name
        /// &lt;= 100 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's phone number
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The customer's device details
        /// </summary>
        public CustomerDevice Device { get; set; }

        /// <summary>
        /// Details of the account the customer holds with the merchant
        /// </summary>
        public MerchantAccount MerchantAccount { get; set; }
    }

    public class CustomerEmail
    {
        /// <summary>
        /// The customer's email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Specifies whether the customer's email address is verified
        /// </summary>
        public bool? Verified { get; set; }
    }

    public class CustomerDevice
    {
        /// <summary>
        /// The device's locale, as an ISO 639-2 language code
        /// </summary>
        public string Locale { get; set; }
    }
}