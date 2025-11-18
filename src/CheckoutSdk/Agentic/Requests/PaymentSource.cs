using Newtonsoft.Json;
using Checkout.Common;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Payment Source for Agentic Enrollment
    /// </summary>
    public class PaymentSource
    {
        /// <summary>
        /// Card number
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Card expiry month (1-12)
        /// </summary>
        [JsonProperty("expiry_month")]
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// Card expiry year (e.g., 2025)
        /// </summary>
        [JsonProperty("expiry_year")]
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// Card verification value (CVV)
        /// </summary>
        [JsonProperty("cvv")]
        public string Cvv { get; set; }

        /// <summary>
        /// Payment source type
        /// </summary>
        [JsonProperty("type")]
        public PaymentSourceType? Type { get; set; }
    }
}