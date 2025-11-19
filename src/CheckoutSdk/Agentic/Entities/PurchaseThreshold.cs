using Checkout.Common;

namespace Checkout.Agentic.Entities
{
    /// <summary>
    /// Purchase threshold configuration
    /// </summary>
    public class PurchaseThreshold
    {
        /// <summary>
        /// The threshold amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency for the threshold
        /// </summary>
        public Currency? CurrencyCode { get; set; }
    }
}