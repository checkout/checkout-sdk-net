using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of asset captured during an ID document verification attempt.
    /// </summary>
    public enum IdDocumentVerificationAttemptAssetType
    {
        [EnumMember(Value = "document_front_image")]
        DocumentFrontImage,

        [EnumMember(Value = "document_back_image")]
        DocumentBackImage
    }
}
