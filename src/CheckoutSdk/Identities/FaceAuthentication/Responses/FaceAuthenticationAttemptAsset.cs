using Checkout.Identities.Entities;
using Newtonsoft.Json;

namespace Checkout.Identities.FaceAuthentication.Responses
{
    /// <summary>
    /// An asset (face image or video) captured during a face authentication attempt.
    /// </summary>
    public class FaceAuthenticationAttemptAsset
    {
        /// <summary>
        /// The type of asset.
        /// [Required]
        /// </summary>
        public FaceAuthenticationAttemptAssetType? Type { get; set; }

        /// <summary>
        /// The links related to the asset.
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public AttemptAssetLinks Links { get; set; }
    }
}
