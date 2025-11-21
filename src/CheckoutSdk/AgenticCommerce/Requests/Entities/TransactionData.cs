using Checkout.Common;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// List of transaction data for the purchase
    /// </summary>
    public class TransactionData
    {
        /// <summary>
        /// The merchant's country, as a two-letter ISO 3166 country code
        /// (https://www.checkout.com/docs/resources/codes/country-codes)
        /// [Required]
        /// </summary>
        public CountryCode? MerchantCountryCode { get; set; }

        /// <summary>
        /// The merchant's name
        /// [Required]
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// The merchant's category code
        /// (https://www.checkout.com/docs/developer-resources/codes/merchant-category-codes)
        /// [Required]
        /// </summary>
        public string MerchantCategoryCode { get; set; }

        /// <summary>
        /// Transaction amount information
        /// [Required]
        /// </summary>
        public TransactionAmount TransactionAmount { get; set; }
        
        /// <summary>
        /// The merchant's website URL
        /// [Optional]
        /// </summary>
        public string MerchantUrl { get; set; }
    }
}