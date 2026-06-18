using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of asset captured during a face authentication attempt.
    /// </summary>
    public enum FaceAuthenticationAttemptAssetType
    {
        [EnumMember(Value = "face_image")]
        FaceImage,

        [EnumMember(Value = "face_video")]
        FaceVideo
    }
}
