namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Create an agentic commerce purchase intent
    /// </summary>
    public class PurchaseIntentCreateRequest : PurchaseIntentUpdateRequest
    {
        /// <summary>
        /// The unique identifier for the network token
        /// [Required]
        /// </summary>
        public string NetworkTokenId { get; set; }

        /// <summary>
        /// The user's device
        /// [Required]
        /// </summary>
        public AgenticDevice Device { get; set; }
    }
}
