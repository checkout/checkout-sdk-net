using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Customer
    {
        /// <summary>
        /// Details of the customer's email
        /// [Optional]
        /// </summary>
        public CustomerEmail Email { get; set; }

        /// <summary>
        /// The customer's full name
        /// [Optional]
        /// &lt;= 100 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's tax identification number.
        /// [Optional]
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// The customer's phone number
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The customer's billing address.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The customer's device details
        /// [Optional]
        /// </summary>
        public CustomerDevice Device { get; set; }

        /// <summary>
        /// Details of the account the customer holds with the merchant
        /// [Optional]
        /// </summary>
        public MerchantAccount MerchantAccount { get; set; }
    }

    public class CustomerEmail
    {
        /// <summary>
        /// The customer's email address
        /// [Optional]
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Specifies whether the customer's email address is verified
        /// [Optional]
        /// </summary>
        public bool? Verified { get; set; }
    }

    public class CustomerDevice
    {
        /// <summary>
        /// The device's locale, as an ISO 639-2 language code
        /// [Optional]
        /// </summary>
        public string Locale { get; set; }
    }
}