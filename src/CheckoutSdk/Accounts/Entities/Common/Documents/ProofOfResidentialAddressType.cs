using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as a representative's proof of residential address (EEA Sole
    /// Trader Full (3.0)). Carries the same <c>proof_of_address</c> value as
    /// <see cref="ProofOfPrincipalAddressType"/>, but the API defines the two as separate enums on
    /// separate documents.
    /// </summary>
    public enum ProofOfResidentialAddressType
    {
        [EnumMember(Value = "proof_of_address")]
        ProofOfAddress
    }
}