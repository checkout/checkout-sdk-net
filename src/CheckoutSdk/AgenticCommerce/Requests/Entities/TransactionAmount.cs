using Checkout.Common;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Transaction amount information
    /// </summary>
    public class TransactionAmount
    {
        /// <summary>
        /// The transaction amount
        /// Format the amount (https://www.checkout.com/docs/payments/accept-payments/format-the-amount-value)
        /// according to the currency_code
        /// [Required]
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The transaction currency, as an ISO 4217 currency code
        /// (https://www.checkout.com/docs/resources/codes/currency-codes)
        /// [Required]
        /// </summary>
        public Currency? CurrencyCode { get; set; }
    }
}