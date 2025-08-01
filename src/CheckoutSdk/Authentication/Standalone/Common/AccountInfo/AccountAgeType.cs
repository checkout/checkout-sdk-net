using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.AccountInfo
{
    public enum AccountAgeType
    {
        [EnumMember(Value = "no_account")]
        NoAccount,

        [EnumMember(Value = "created_during_transaction")]
        CreatedDuringTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }
}