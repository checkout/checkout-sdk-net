using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as proof of legality.
    /// </summary>
    public enum ProofOfLegalityType
    {
        [EnumMember(Value = "proof_of_legality")]
        ProofOfLegality
    }
}