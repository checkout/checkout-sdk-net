using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Card.Metadata
{
    public enum CardType
    {
        [EnumMember(Value = "CREDIT")]
        CREDIT,

        [EnumMember(Value = "DEBIT")]
        DEBIT,

        [EnumMember(Value = "PREPAID")]
        PREPAID,

        [EnumMember(Value = "CHARGE")]
        CHARGE,

        [EnumMember(Value = "DEFERRED DEBIT")]
        DEFERREDDEBIT,
    }
}