namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Request to create a purchase intent for agentic commerce
    /// </summary>
    public class AgenticCreatePurchaseIntentRequest
    {
        /// <summary>
        /// The network token ID
        /// </summary>
        public string NetworkTokenId { get; set; }

        /// <summary>
        /// The device information
        /// </summary>
        public DeviceInfo Device { get; set; }

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
