using Newtonsoft.Json;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Custom headers required for the Agentic Commerce delegated payment endpoint.
    /// </summary>
    public class DelegatedPaymentHeaders : IHeaders
    {
        /// <summary>
        /// A Base64-encoded HMAC-SHA256 signature used for request body integrity verification.
        /// Computed as: Base64(HMAC-SHA256(key, Timestamp + RequestBody))
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "Signature")]
        public string Signature { get; set; }

        /// <summary>
        /// The timestamp of the request, in RFC 3339 format (for example, 2026-03-11T10:30:00Z).
        /// Must be within 5 minutes of the server time.
        /// [Required]
        /// Format: date-time.
        /// </summary>
        [JsonProperty(PropertyName = "Timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// The API version to use for the request. If not specified, the default version is used.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "API-Version")]
        public string ApiVersion { get; set; }
    }
}
