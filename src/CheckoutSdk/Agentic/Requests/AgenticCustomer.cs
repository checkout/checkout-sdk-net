using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Customer information for agentic enrollment
    /// </summary>
    public class AgenticCustomer
    {
        /// <summary>
        /// Customer email address
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Customer country code (ISO 3166-1 alpha-2)
        /// </summary>
        [JsonProperty("country_code")]
        public CountryCode CountryCode { get; set; }

        /// <summary>
        /// Customer language code (ISO 639-1)
        /// </summary>
        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }
    }
}