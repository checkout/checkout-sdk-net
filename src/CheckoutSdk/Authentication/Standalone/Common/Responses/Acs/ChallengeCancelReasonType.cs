using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Acs
{
    public enum ChallengeCancelReasonType
    {
        [EnumMember(Value = "cardholder_cancel")]
        CardholderCancel,

        [EnumMember(Value = "transaction_timed_out")]
        TransactionTimedOut,

        [EnumMember(Value = "challenge_timed_out")]
        ChallengeTimedOut,

        [EnumMember(Value = "transaction_error")]
        TransactionError,

        [EnumMember(Value = "unknown")]
        Unknown,

        [EnumMember(Value = "sdk_timed_out")]
        SdkTimedOut,
    }
}