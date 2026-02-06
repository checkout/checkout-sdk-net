using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    ///     Response for retrieving identity verification attempts
    /// </summary>
    public class IdentityVerificationAttemptsResponse : Resource
    {
        /// <summary>
        ///     List of attempts for the identity verification
        /// </summary>
        [JsonProperty(PropertyName = "attempts")]
        public List<IdentityVerificationAttempt> Attempts { get; set; }

        /// <summary>
        ///     Total number of attempts
        /// </summary>
        [JsonProperty(PropertyName = "total_count")]
        public int? TotalCount { get; set; }
    }

    /// <summary>
    ///     Individual attempt information
    /// </summary>
    public class IdentityVerificationAttempt
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
        ///     The creation timestamp
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     The last updated timestamp
        /// </summary>
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     The expiry time for the attempt URL
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public string ExpiresAt { get; set; }
    }

    /// <summary>
    ///     Status of an identity verification attempt
    /// </summary>
    public enum IdentityVerificationAttemptStatus
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
}