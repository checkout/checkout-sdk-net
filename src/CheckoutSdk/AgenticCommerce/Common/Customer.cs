using Checkout.Common;

namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// The customer's details
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The customer's email address
        /// [Required]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's country, as a two-letter ISO 3166 country code
        /// (https://www.checkout.com/docs/resources/codes/country-codes)
        /// [Required]
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// The customer's language, as a two-letter ISO 639 language code
        /// [Required]
        /// </summary>
        public string LanguageCode { get; set; }
    }
}