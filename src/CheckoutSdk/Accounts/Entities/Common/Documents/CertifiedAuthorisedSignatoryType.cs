using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as a representative's certified authorised signatory document.
    /// </summary>
    public enum CertifiedAuthorisedSignatoryType
    {
        [EnumMember(Value = "power_of_attorney")]
        PowerOfAttorney
    }
}