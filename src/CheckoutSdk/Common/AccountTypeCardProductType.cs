using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum AccountTypeCardProductType
    {
        [EnumMember(Value = "credit")] Credit,
        [EnumMember(Value = "debit")] Debit,
        [EnumMember(Value = "not_applicable")] NotApplicable
    }
}