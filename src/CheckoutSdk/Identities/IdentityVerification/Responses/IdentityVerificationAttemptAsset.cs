using Checkout.Identities.Entities;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    /// An asset (face image, video, or document image) captured during an identity verification attempt.
    /// </summary>
    public class IdentityVerificationAttemptAsset
    {
        /// <summary>
        /// The type of asset.
        /// [Required]
        /// </summary>
        public IdentityVerificationAttemptAssetType? Type { get; set; }

        /// <summary>
        /// The links related to the asset.
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public AttemptAssetLinks Links { get; set; }
    }
}
