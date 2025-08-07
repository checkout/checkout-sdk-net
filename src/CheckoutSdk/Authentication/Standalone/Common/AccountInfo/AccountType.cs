using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.AccountInfo
{
    public enum AccountType
    {
        [EnumMember(Value = "not_applicable")]
        NotApplicable,

        [EnumMember(Value = "credit")]
        Credit,

        [EnumMember(Value = "debit")]
        Debit,
    }
}