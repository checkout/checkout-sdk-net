using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum AccountChangeIndicatorType
    {
        [EnumMember(Value = "this_transaction")]
        ThisTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays
    }
}