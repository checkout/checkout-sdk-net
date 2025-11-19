using Checkout.Agentic.Entities;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Request to create a purchase intent credentials for agentic commerce
    /// </summary>
    public class AgenticPurchaseIntentCredentialsCreateRequest
    {
        /// <summary>
        /// Array of transaction data for the purchase intent credentials
        /// </summary>
        public TransactionData[] TransactionData { get; set; }
    }
}
