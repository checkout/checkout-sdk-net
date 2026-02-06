using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Requests
{
    /// <summary>
    ///     Request for creating an identity verification with an initial attempt
    /// </summary>
    public class IdentityVerificationCreateAndOpenRequest
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

        /// <summary>
        ///     Method to create an attempt
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public IdentityVerificationMethod Method { get; set; }
    }

    /// <summary>
    ///     Personal information for the applicant
    /// </summary>
    public class IdentityVerificationApplicant
    {
        /// <summary>
        ///     The applicant's first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     The applicant's last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     The applicant's date of birth
        /// </summary>
        [JsonProperty(PropertyName = "date_of_birth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     The applicant's nationality
        /// </summary>
        [JsonProperty(PropertyName = "nationality")]
        public string Nationality { get; set; }

        /// <summary>
        ///     The applicant's address
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public IdentityVerificationAddress Address { get; set; }

        /// <summary>
        ///     The applicant's identification number
        /// </summary>
        [JsonProperty(PropertyName = "id_number")]
        public string IdNumber { get; set; }
    }

    /// <summary>
    ///     Address information for the applicant
    /// </summary>
    public class IdentityVerificationAddress
    {
        /// <summary>
        ///     The address line
        /// </summary>
        [JsonProperty(PropertyName = "line1")]
        public string Line1 { get; set; }

        /// <summary>
        ///     The second address line
        /// </summary>
        [JsonProperty(PropertyName = "line2")]
        public string Line2 { get; set; }

        /// <summary>
        ///     The city
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        ///     The state or province
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        ///     The postal code
        /// </summary>
        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set; }

        /// <summary>
        ///     The country code
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }

    /// <summary>
    ///     Options for document types to be uploaded, selfies, and other IDV checks to be performed
    /// </summary>
    public class IdentityVerificationOptions
    {
        /// <summary>
        ///     Document types to be uploaded
        /// </summary>
        [JsonProperty(PropertyName = "document_types")]
        public List<string> DocumentTypes { get; set; }

        /// <summary>
        ///     Whether a selfie should be taken
        /// </summary>
        [JsonProperty(PropertyName = "selfie_check")]
        public bool? SelfieCheck { get; set; }

        /// <summary>
        ///     Whether a liveness check should be performed
        /// </summary>
        [JsonProperty(PropertyName = "liveness_check")]
        public bool? LivenessCheck { get; set; }

        /// <summary>
        ///     Whether document authenticity should be checked
        /// </summary>
        [JsonProperty(PropertyName = "document_authenticity_check")]
        public bool? DocumentAuthenticityCheck { get; set; }

        /// <summary>
        ///     Whether data comparison should be performed
        /// </summary>
        [JsonProperty(PropertyName = "data_comparison_check")]
        public bool? DataComparisonCheck { get; set; }
    }

    /// <summary>
    ///     Response webhook URL configuration
    /// </summary>
    public class IdentityVerificationWebhook
    {
        /// <summary>
        ///     The webhook URL
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    /// <summary>
    ///     Supported locales for identity verification
    /// </summary>
    public enum IdentityVerificationLocale
    {
        [JsonProperty(PropertyName = "en")]
        English,
        [JsonProperty(PropertyName = "es")]
        Spanish,
        [JsonProperty(PropertyName = "fr")]
        French,
        [JsonProperty(PropertyName = "de")]
        German,
        [JsonProperty(PropertyName = "it")]
        Italian,
        [JsonProperty(PropertyName = "pt")]
        Portuguese,
        [JsonProperty(PropertyName = "nl")]
        Dutch,
        [JsonProperty(PropertyName = "sv")]
        Swedish,
        [JsonProperty(PropertyName = "da")]
        Danish,
        [JsonProperty(PropertyName = "no")]
        Norwegian,
        [JsonProperty(PropertyName = "fi")]
        Finnish,
        [JsonProperty(PropertyName = "pl")]
        Polish
    }

    /// <summary>
    ///     Method to create an attempt
    /// </summary>
    public class IdentityVerificationMethod
    {
        /// <summary>
        ///     The type of method
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = "hosted";

        /// <summary>
        ///     Configuration for the method
        /// </summary>
        [JsonProperty(PropertyName = "config")]
        public IdentityVerificationMethodConfig Config { get; set; }
    }

    /// <summary>
    ///     Configuration for the identity verification method
    /// </summary>
    public class IdentityVerificationMethodConfig
    {
        /// <summary>
        ///     The redirect URL after completion
        /// </summary>
        [JsonProperty(PropertyName = "redirect_url")]
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     The cancel URL
        /// </summary>
        [JsonProperty(PropertyName = "cancel_url")]
        public string CancelUrl { get; set; }

        /// <summary>
        ///     The theme color
        /// </summary>
        [JsonProperty(PropertyName = "theme")]
        public string Theme { get; set; }

        /// <summary>
        ///     The logo URL
        /// </summary>
        [JsonProperty(PropertyName = "logo_url")]
        public string LogoUrl { get; set; }
    }
}