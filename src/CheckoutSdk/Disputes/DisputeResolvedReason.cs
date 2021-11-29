using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum DisputeResolvedReason
    {
        [EnumMember(Value = "rapid_dispute_resolution")]
        RapidDisputeResolution,

        [EnumMember(Value = "negative_amount")]
        NegativeAmount,

        [EnumMember(Value = "already_refunded")]
        AlreadyRefunded
    }
}