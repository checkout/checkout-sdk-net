using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum Type
    {
        [EnumMember(Value = "authorization")]
        Authorization,

        [EnumMember(Value = "reversal")]
        Reversal,

        [EnumMember(Value = "authorization_advice")]
        AuthorizationAdvice,

        [EnumMember(Value = "reversal_advice")]
        ReversalAdvice,

        [EnumMember(Value = "presentment")]
        Presentment,

        [EnumMember(Value = "authorization_refund")]
        AuthorizationRefund,

        [EnumMember(Value = "presentment_refund")]
        PresentmentRefund,

        [EnumMember(Value = "presentment_reversed")]
        PresentmentReversed,

        [EnumMember(Value = "presentment_reversed_refund")]
        PresentmentReversedRefund
    }
}