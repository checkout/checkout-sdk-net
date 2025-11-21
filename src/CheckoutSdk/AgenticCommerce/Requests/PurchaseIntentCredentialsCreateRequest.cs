using System.Collections.Generic;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Request to create a purchase intent credentials for agentic commerce
    /// </summary>
    public class PurchaseIntentCredentialsCreateRequest
    {
        /// <summary>
        /// Array of transaction data for the purchase intent credentials
        /// </summary>
        public IList<TransactionData> TransactionData { get; set; }
    }
}
