using System;
using Newtonsoft.Json;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Agentic Enroll Response
    /// </summary>
    public class AgenticEnrollResponse : HttpMetadata
    {
        /// <summary>
        /// The unique token identifier for the enrolled agentic service
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; }

        /// <summary>
        /// Current status of the enrollment (e.g., "enrolled")
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the enrollment was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}