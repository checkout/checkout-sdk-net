using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as tax verification: an IRS-issued Employer Identification Number
    /// letter.
    /// </summary>
    public enum TaxVerificationType
    {
        [EnumMember(Value = "ein_letter")]
        EinLetter
    }
}