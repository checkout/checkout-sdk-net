using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardholderAccountAgeIndicatorType
    {
        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
        
        [EnumMember(Value = "no_account")] 
        NoAccount,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "this_transaction")]
        ThisTransaction
    }
}