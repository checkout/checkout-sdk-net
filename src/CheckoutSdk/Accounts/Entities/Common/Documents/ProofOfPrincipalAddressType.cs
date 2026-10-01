using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as proof of the company's principal place of business. Carries
    /// the same <c>proof_of_address</c> value as <see cref="ProofOfResidentialAddressType"/>, but the
    /// API defines the two as separate enums on separate documents.
    /// </summary>
    public enum ProofOfPrincipalAddressType
    {
        [EnumMember(Value = "proof_of_address")]
        ProofOfAddress
    }
}