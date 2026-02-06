using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Requests
{
    /// <summary>
    ///     Request for creating a new identity verification attempt
    /// </summary>
    public class IdentityVerificationAttemptRequest
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
}