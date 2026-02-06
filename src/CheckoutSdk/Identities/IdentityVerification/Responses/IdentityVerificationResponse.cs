using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Checkout.Common;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    ///     Response for identity verification operations
    /// </summary>
    public class IdentityVerificationResponse : Resource
    {
        /// <summary>
        ///     The unique identifier for the identity verification
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     The current status of the identity verification
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public IdentityVerificationStatus? Status { get; set; }

        /// <summary>
        ///     The reference for this identity verification
        /// </summary>
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

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
        ///     The applicant information
        /// </summary>
        [JsonProperty(PropertyName = "applicant")]
        public IdentityVerificationApplicantResponse Applicant { get; set; }

        /// <summary>
        ///     The verification options
        /// </summary>
        [JsonProperty(PropertyName = "options")]
        public IdentityVerificationOptionsResponse Options { get; set; }

        /// <summary>
        ///     The webhook configuration
        /// </summary>
        [JsonProperty(PropertyName = "webhook")]
        public IdentityVerificationWebhookResponse Webhook { get; set; }

        /// <summary>
        ///     Whether dark mode is enabled
        /// </summary>
        [JsonProperty(PropertyName = "dark_mode")]
        public bool? DarkMode { get; set; }

        /// <summary>
        ///     The locale setting
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        /// <summary>
        ///     Verification results and findings
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public IdentityVerificationResult Result { get; set; }

        /// <summary>
        ///     List of attempts made for this verification
        /// </summary>
        [JsonProperty(PropertyName = "attempts")]
        public List<IdentityVerificationAttemptSummary> Attempts { get; set; }

        /// <summary>
        ///     The creation timestamp
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     The last updated timestamp
        /// </summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    ///     Status of an identity verification
    /// </summary>
    public enum IdentityVerificationStatus
    {
        [JsonProperty(PropertyName = "pending")]
        Pending,
        [JsonProperty(PropertyName = "in_progress")]
        InProgress,
        [JsonProperty(PropertyName = "completed")]
        Completed,
        [JsonProperty(PropertyName = "failed")]
        Failed,
        [JsonProperty(PropertyName = "expired")]
        Expired,
        [JsonProperty(PropertyName = "canceled")]
        Canceled
    }

    /// <summary>
    ///     Applicant response information
    /// </summary>
    public class IdentityVerificationApplicantResponse
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
        public IdentityVerificationAddressResponse Address { get; set; }

        /// <summary>
        ///     The applicant's identification number
        /// </summary>
        [JsonProperty(PropertyName = "id_number")]
        public string IdNumber { get; set; }
    }

    /// <summary>
    ///     Address response information
    /// </summary>
    public class IdentityVerificationAddressResponse
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
    ///     Options response information
    /// </summary>
    public class IdentityVerificationOptionsResponse
    {
        /// <summary>
        ///     Document types configured for upload
        /// </summary>
        [JsonProperty(PropertyName = "document_types")]
        public List<string> DocumentTypes { get; set; }

        /// <summary>
        ///     Whether a selfie check is enabled
        /// </summary>
        [JsonProperty(PropertyName = "selfie_check")]
        public bool? SelfieCheck { get; set; }

        /// <summary>
        ///     Whether a liveness check is enabled
        /// </summary>
        [JsonProperty(PropertyName = "liveness_check")]
        public bool? LivenessCheck { get; set; }

        /// <summary>
        ///     Whether document authenticity check is enabled
        /// </summary>
        [JsonProperty(PropertyName = "document_authenticity_check")]
        public bool? DocumentAuthenticityCheck { get; set; }

        /// <summary>
        ///     Whether data comparison check is enabled
        /// </summary>
        [JsonProperty(PropertyName = "data_comparison_check")]
        public bool? DataComparisonCheck { get; set; }
    }

    /// <summary>
    ///     Webhook response information
    /// </summary>
    public class IdentityVerificationWebhookResponse
    {
        /// <summary>
        ///     The webhook URL
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    /// <summary>
    ///     Verification results and findings
    /// </summary>
    public class IdentityVerificationResult
    {
        /// <summary>
        ///     Overall result of the verification
        /// </summary>
        [JsonProperty(PropertyName = "overall")]
        public string Overall { get; set; }

        /// <summary>
        ///     Document verification results
        /// </summary>
        [JsonProperty(PropertyName = "document")]
        public IdentityVerificationDocumentResult Document { get; set; }

        /// <summary>
        ///     Identity verification results
        /// </summary>
        [JsonProperty(PropertyName = "identity")]
        public IdentityVerificationIdentityResult Identity { get; set; }
    }

    /// <summary>
    ///     Document verification result details
    /// </summary>
    public class IdentityVerificationDocumentResult
    {
        /// <summary>
        ///     Document authenticity result
        /// </summary>
        [JsonProperty(PropertyName = "authenticity")]
        public string Authenticity { get; set; }

        /// <summary>
        ///     Document quality result
        /// </summary>
        [JsonProperty(PropertyName = "quality")]
        public string Quality { get; set; }
    }

    /// <summary>
    ///     Identity verification result details
    /// </summary>
    public class IdentityVerificationIdentityResult
    {
        /// <summary>
        ///     Face match result
        /// </summary>
        [JsonProperty(PropertyName = "face_match")]
        public string FaceMatch { get; set; }

        /// <summary>
        ///     Liveness result
        /// </summary>
        [JsonProperty(PropertyName = "liveness")]
        public string Liveness { get; set; }

        /// <summary>
        ///     Data comparison result
        /// </summary>
        [JsonProperty(PropertyName = "data_comparison")]
        public string DataComparison { get; set; }
    }

    /// <summary>
    ///     Summary information for an attempt
    /// </summary>
    public class IdentityVerificationAttemptSummary
    {
        /// <summary>
        ///     The unique identifier for the attempt
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     The status of the attempt
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        ///     The type of attempt
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     The creation timestamp
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     The last updated timestamp
        /// </summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}