using Newtonsoft.Json;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Agentic Enroll Request
    /// </summary>
    public class AgenticEnrollRequest
    {
        /// <summary>
        /// Payment source information
        /// </summary>
        [JsonProperty("source")]
        public PaymentSource Source { get; set; }

        /// <summary>
        /// Device information for fraud detection and analysis
        /// </summary>
        [JsonProperty("device")]
        public DeviceInfo Device { get; set; }

        /// <summary>
        /// Customer information
        /// </summary>
        [JsonProperty("customer")]
        public AgenticCustomer Customer { get; set; }
    }
}