using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum CertifiedAuthorisedSignatoryType
    {
        [EnumMember(Value = "power_of_attorney")]
        PowerOfAttorney
    }
}