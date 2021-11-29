using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum ActionType
    {
        [EnumMember(Value = "Authorization")] Authorization,

        [EnumMember(Value = "Card Verification")]
        CardVerification,
        [EnumMember(Value = "Void")] Void,
        [EnumMember(Value = "Capture")] Capture,
        [EnumMember(Value = "Refund")] Refund,
        [EnumMember(Value = "Payout")] Payout,
        [EnumMember(Value = "Return")] Return
    }
}