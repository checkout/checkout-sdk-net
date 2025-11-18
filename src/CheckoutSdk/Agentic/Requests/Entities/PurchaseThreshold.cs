namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Purchase threshold configuration
    /// </summary>
    public class PurchaseThreshold
    {
        /// <summary>
        /// The threshold amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency code (e.g., USD, EUR)
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}