using Checkout.Identities.Entities;
using Newtonsoft.Json;

namespace Checkout.Identities.AddressDocumentVerification.Responses
{
    /// <summary>
    /// An asset (the document image) uploaded for an address document verification attempt.
    /// </summary>
    public class AddressDocumentVerificationAttemptAsset
    {
        /// <summary>
        /// The type of asset.
        /// [Required]
        /// </summary>
        public AddressDocumentVerificationAttemptAssetType? Type { get; set; }

        /// <summary>
        /// The links related to the asset.
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public AttemptAssetLinks Links { get; set; }
    }
}
