using Checkout.Common;

namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// Purchase threshold configuration
    /// </summary>
    public class PurchaseThreshold
    {
        /// <summary>
        /// The maximum amount for the purchase
        /// Format the amount (https://www.checkout.com/docs/payments/accept-payments/format-the-amount-value)
        /// according to the currency_code
        /// [Required]
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency to use for the purchase, as an ISO 4217 currency code
        /// (https://www.checkout.com/docs/resources/codes/currency-codes)
        /// [Required]
        /// </summary>
        public Currency? CurrencyCode { get; set; }
    }
}