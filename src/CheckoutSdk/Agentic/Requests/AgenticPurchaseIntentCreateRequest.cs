using Checkout.Agentic.Entities;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Request to create a purchase intent for agentic commerce
    /// </summary>
    public class AgenticPurchaseIntentCreateRequest : AgenticPurchaseIntentUpdateRequest
    {
        /// <summary>
        /// The network token ID
        /// </summary>
        public string NetworkTokenId { get; set; }

        /// <summary>
        /// The device information
        /// </summary>
        public DeviceInfo Device { get; set; }
    }
}
