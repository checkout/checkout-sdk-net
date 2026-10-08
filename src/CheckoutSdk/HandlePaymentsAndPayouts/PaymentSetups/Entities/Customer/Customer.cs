using Checkout.Common;
using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's details.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Details of the customer's email.
        /// [Optional]
        /// </summary>
        public CustomerEmail Email { get; set; }

        /// <summary>
        /// The customer's full name.
        /// [Optional]
        /// max 100 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's tax identification number.
        /// [Optional]
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The customer's billing address. Removed from the Payment Setup customer schema on
        /// 2026-04-08; send the billing details through PaymentSetupsRequest.Billing instead.
        /// [Optional]
        /// </summary>
        [Obsolete("Removed from the Payment Setup customer schema on 2026-04-08. Use PaymentSetupsRequest.Billing instead. Will be removed in a future major version.")]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Details of the customer's device.
        /// [Optional]
        /// </summary>
        public CustomerDevice Device { get; set; }

        /// <summary>
        /// Details of the account the customer holds with the merchant.
        /// [Optional]
        /// </summary>
        public MerchantAccount MerchantAccount { get; set; }

        /// <summary>
        /// The unique identifier of the customer.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the customer for this payment.
        /// [Optional]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
