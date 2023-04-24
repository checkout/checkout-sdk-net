using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum ChallengeCancelReason
    {
        [EnumMember(Value = "cardholder_cancel")]
        CardholderCancel,

        [EnumMember(Value = "transaction_timed_out")]
        TransactionTimedOut,

        [EnumMember(Value = "challenge_timed_out")]
        ChallengeTimedOut,

        [EnumMember(Value = "transaction_error")]
        TransactionError,
        [EnumMember(Value = "sdk_timed_out")] SdkTimedOut,
        [EnumMember(Value = "unknown")] Unknown
    }
}