using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of asset captured during an address document verification attempt.
    /// </summary>
    public enum AddressDocumentVerificationAttemptAssetType
    {
        [EnumMember(Value = "document")]
        Document
    }
}
