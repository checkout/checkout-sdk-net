using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as a certified shareholder structure.
    /// </summary>
    public enum ShareholderStructureType
    {
        [EnumMember(Value = "certified_shareholder_structure")]
        CertifiedShareholderStructure
    }
}