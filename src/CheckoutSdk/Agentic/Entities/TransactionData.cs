using Checkout.Common;

namespace Checkout.Agentic.Entities
{
    /// <summary>
    /// Represents transaction data for agentic commerce
    /// </summary>
    public class TransactionData
    {
        /// <summary>
        /// Merchant country code (e.g., US, GB)
        /// </summary>
        public CountryCode? MerchantCountryCode { get; set; }

        /// <summary>
        /// Merchant name
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// Merchant category code
        /// </summary>
        public string MerchantCategoryCode { get; set; }

        /// <summary>
        /// Merchant website URL
        /// </summary>
        public string MerchantUrl { get; set; }

        /// <summary>
        /// Transaction amount information
        /// </summary>
        public TransactionAmount TransactionAmount { get; set; }
    }
}