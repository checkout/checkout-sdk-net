using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// A code that provides more information about a risk associated with the verification.
    /// </summary>
    public enum RiskLabel
    {
        [EnumMember(Value = "multiple_faces_detected")]
        MultipleFacesDetected,

        [EnumMember(Value = "mcc_not_confident")]
        MccNotConfident,

        [EnumMember(Value = "risky_document_format")]
        RiskyDocumentFormat
    }
}
