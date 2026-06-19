using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of asset captured during an identity verification attempt.
    /// </summary>
    public enum IdentityVerificationAttemptAssetType
    {
        [EnumMember(Value = "face_image")]
        FaceImage,

        [EnumMember(Value = "face_video")]
        FaceVideo,

        [EnumMember(Value = "document_front_image")]
        DocumentFrontImage,

        [EnumMember(Value = "document_back_image")]
        DocumentBackImage,

        [EnumMember(Value = "document_front_video")]
        DocumentFrontVideo,

        [EnumMember(Value = "document_back_video")]
        DocumentBackVideo,

        [EnumMember(Value = "document_signature_image")]
        DocumentSignatureImage,

        [EnumMember(Value = "secondary_document_front_image")]
        SecondaryDocumentFrontImage,

        [EnumMember(Value = "secondary_document_back_image")]
        SecondaryDocumentBackImage,

        [EnumMember(Value = "secondary_document_front_video")]
        SecondaryDocumentFrontVideo,

        [EnumMember(Value = "secondary_document_back_video")]
        SecondaryDocumentBackVideo,

        [EnumMember(Value = "secondary_document_signature_image")]
        SecondaryDocumentSignatureImage
    }
}
