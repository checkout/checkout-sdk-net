using System;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    ///     Response for creating an identity verification with an initial attempt
    /// </summary>
    public class IdentityVerificationCreateAndOpenResponse : Resource
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
        ///     The attempt URL for the hosted verification
        /// </summary>
        [JsonProperty(PropertyName = "attempt_url")]
        public string AttemptUrl { get; set; }

        /// <summary>
        ///     The attempt ID
        /// </summary>
        [JsonProperty(PropertyName = "attempt_id")]
        public string AttemptId { get; set; }

        /// <summary>
        ///     The expiry time for the attempt URL
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }
}