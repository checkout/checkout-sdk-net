using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Requests
{
    /// <summary>
    ///     Request for creating a new identity verification
    /// </summary>
    public class IdentityVerificationRequest
    {
        /// <summary>
        ///     The source that created the IDV resource
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        /// <summary>
        ///     The type of identity verification
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     Reference to the applicant associated with the IDV
        /// </summary>
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

        /// <summary>
        ///     Personal information for the applicant
        /// </summary>
        [JsonProperty(PropertyName = "applicant")]
        public IdentityVerificationApplicant Applicant { get; set; }

        /// <summary>
        ///     Options for document types to be uploaded, selfies, and other IDV checks to be performed
        /// </summary>
        [JsonProperty(PropertyName = "options")]
        public IdentityVerificationOptions Options { get; set; }

        /// <summary>
        ///     Response webhook URL
        /// </summary>
        [JsonProperty(PropertyName = "webhook")]
        public IdentityVerificationWebhook Webhook { get; set; }

        /// <summary>
        ///     Enables dark mode styling
        /// </summary>
        [JsonProperty(PropertyName = "dark_mode")]
        public bool? DarkMode { get; set; }

        /// <summary>
        ///     Override default language
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public IdentityVerificationLocale? Locale { get; set; }
    }
}