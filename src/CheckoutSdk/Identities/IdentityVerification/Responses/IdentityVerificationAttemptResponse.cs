using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Checkout.Common;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    ///     Response for identity verification attempt operations
    /// </summary>
    public class IdentityVerificationAttemptResponse : Resource
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
        public IdentityVerificationAttemptStatus? Status { get; set; }

        /// <summary>
        ///     The type of attempt method
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     The URL for hosted attempts
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        ///     Whether the attempt is complete
        /// </summary>
        [JsonProperty(PropertyName = "complete")]
        public bool? Complete { get; set; }

        /// <summary>
        ///     Configuration used for this attempt
        /// </summary>
        [JsonProperty(PropertyName = "config")]
        public IdentityVerificationAttemptConfig Config { get; set; }

        /// <summary>
        ///     Results from the verification attempt
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public IdentityVerificationAttemptResult Result { get; set; }

        /// <summary>
        ///     Documents uploaded during the attempt
        /// </summary>
        [JsonProperty(PropertyName = "documents")]
        public List<IdentityVerificationDocument> Documents { get; set; }

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

        /// <summary>
        ///     The expiry time for the attempt URL
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }

    /// <summary>
    ///     Configuration for an identity verification attempt
    /// </summary>
    public class IdentityVerificationAttemptConfig
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

    /// <summary>
    ///     Results from an identity verification attempt
    /// </summary>
    public class IdentityVerificationAttemptResult
    {
        /// <summary>
        ///     Overall result of the attempt
        /// </summary>
        [JsonProperty(PropertyName = "overall")]
        public string Overall { get; set; }

        /// <summary>
        ///     Score from 0 to 1 indicating confidence in the verification
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public decimal? Score { get; set; }

        /// <summary>
        ///     Detailed breakdown of verification checks
        /// </summary>
        [JsonProperty(PropertyName = "breakdown")]
        public IdentityVerificationBreakdown Breakdown { get; set; }
    }

    /// <summary>
    ///     Detailed breakdown of verification checks
    /// </summary>
    public class IdentityVerificationBreakdown
    {
        /// <summary>
        ///     Document verification results
        /// </summary>
        [JsonProperty(PropertyName = "document")]
        public IdentityVerificationDocumentBreakdown Document { get; set; }

        /// <summary>
        ///     Identity verification results
        /// </summary>
        [JsonProperty(PropertyName = "identity")]
        public IdentityVerificationIdentityBreakdown Identity { get; set; }
    }

    /// <summary>
    ///     Document verification breakdown
    /// </summary>
    public class IdentityVerificationDocumentBreakdown
    {
        /// <summary>
        ///     Document authenticity check result
        /// </summary>
        [JsonProperty(PropertyName = "authenticity")]
        public IdentityVerificationCheckResult Authenticity { get; set; }

        /// <summary>
        ///     Document quality check result
        /// </summary>
        [JsonProperty(PropertyName = "quality")]
        public IdentityVerificationCheckResult Quality { get; set; }
    }

    /// <summary>
    ///     Identity verification breakdown
    /// </summary>
    public class IdentityVerificationIdentityBreakdown
    {
        /// <summary>
        ///     Face match check result
        /// </summary>
        [JsonProperty(PropertyName = "face_match")]
        public IdentityVerificationCheckResult FaceMatch { get; set; }

        /// <summary>
        ///     Liveness check result
        /// </summary>
        [JsonProperty(PropertyName = "liveness")]
        public IdentityVerificationCheckResult Liveness { get; set; }

        /// <summary>
        ///     Data comparison check result
        /// </summary>
        [JsonProperty(PropertyName = "data_comparison")]
        public IdentityVerificationCheckResult DataComparison { get; set; }
    }

    /// <summary>
    ///     Individual check result
    /// </summary>
    public class IdentityVerificationCheckResult
    {
        /// <summary>
        ///     Result of the check
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        /// <summary>
        ///     Score from 0 to 1 indicating confidence
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public decimal? Score { get; set; }

        /// <summary>
        ///     Reason for the result
        /// </summary>
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }
    }

    /// <summary>
    ///     Document information from verification attempt
    /// </summary>
    public class IdentityVerificationDocument
    {
        /// <summary>
        ///     The unique identifier for the document
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     The type of document
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     The side of the document (front/back)
        /// </summary>
        [JsonProperty(PropertyName = "side")]
        public string Side { get; set; }

        /// <summary>
        ///     The country of the document
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        ///     Extracted data from the document
        /// </summary>
        [JsonProperty(PropertyName = "extracted_data")]
        public IdentityVerificationExtractedData ExtractedData { get; set; }
    }

    /// <summary>
    ///     Data extracted from a document
    /// </summary>
    public class IdentityVerificationExtractedData
    {
        /// <summary>
        ///     Extracted first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Extracted last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     Extracted date of birth
        /// </summary>
        [JsonProperty(PropertyName = "date_of_birth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     Extracted nationality
        /// </summary>
        [JsonProperty(PropertyName = "nationality")]
        public string Nationality { get; set; }

        /// <summary>
        ///     Extracted document number
        /// </summary>
        [JsonProperty(PropertyName = "document_number")]
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     Extracted expiry date
        /// </summary>
        [JsonProperty(PropertyName = "expiry_date")]
        public string ExpiryDate { get; set; }
    }
}