using Checkout.Identities.Entities;
using Newtonsoft.Json;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    /// <summary>
    /// An asset (the front or back image of the document) uploaded for an ID document
    /// verification attempt.
    /// </summary>
    public class IdDocumentVerificationAttemptAsset
    {
        /// <summary>
        /// The type of asset.
        /// [Required]
        /// </summary>
        public IdDocumentVerificationAttemptAssetType? Type { get; set; }

        /// <summary>
        /// The links related to the asset.
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public AttemptAssetLinks Links { get; set; }
    }
}
