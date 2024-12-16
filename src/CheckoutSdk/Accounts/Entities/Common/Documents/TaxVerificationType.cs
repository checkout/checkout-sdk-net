using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum TaxVerificationType
    {
        [EnumMember(Value = "ein_letter")] EinLetter
    }
}