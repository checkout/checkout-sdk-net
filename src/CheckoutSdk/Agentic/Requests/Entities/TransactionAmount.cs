using Checkout.Common;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Represents transaction amount information for agentic commerce
    /// </summary>
    public class TransactionAmount
    {
        /// <summary>
        /// Transaction amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Currency for the transaction
        /// </summary>
        public Currency? CurrencyCode { get; set; }
    }
}