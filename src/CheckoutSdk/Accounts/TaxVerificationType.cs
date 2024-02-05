using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum TaxVerificationType
    {
        [EnumMember(Value = "ein_letter")] EinLetter
    }
}