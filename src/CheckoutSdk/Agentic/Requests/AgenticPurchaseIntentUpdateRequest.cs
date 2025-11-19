using Checkout.Agentic.Entities;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Request to update a purchase intent for agentic commerce
    /// </summary>
    public class AgenticPurchaseIntentUpdateRequest
    {
        /// <summary>
        /// Customer prompt describing the purchase intent
        /// </summary>
        public string CustomerPrompt { get; set; }

        /// <summary>
        /// List of mandates for the purchase intent
        /// </summary>
        public Mandate[] Mandates { get; set; }
    }
}
