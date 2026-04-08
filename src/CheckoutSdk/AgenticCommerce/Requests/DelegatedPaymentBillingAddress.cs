using Checkout.Common;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// The customer billing address associated with the delegated payment.
    /// </summary>
    public class DelegatedPaymentBillingAddress
    {
        /// <summary>
        /// The full name of the customer.
        /// [Required]
        /// &lt;= 256 characters.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The first line of the street address.
        /// [Required]
        /// &lt;= 60 characters.
        /// </summary>
        public string LineOne { get; set; }

        /// <summary>
        /// The second line of the street address.
        /// [Optional]
        /// &lt;= 60 characters.
        /// </summary>
        public string LineTwo { get; set; }

        /// <summary>
        /// The city of the billing address.
        /// [Required]
        /// &lt;= 60 characters.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state, county, province, or region of the billing address.
        /// [Optional]
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The postal code or ZIP code of the billing address.
        /// [Required]
        /// &lt;= 20 characters.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The two-letter ISO 3166-1 alpha-2 country code.
        /// [Required]
        /// 2 characters.
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
