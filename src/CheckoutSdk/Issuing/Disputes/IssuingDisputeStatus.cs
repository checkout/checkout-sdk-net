using System.Runtime.Serialization;

namespace Checkout.Issuing.Disputes
{
    public enum IssuingDisputeStatus
    {
        [EnumMember(Value = "created")] Created,
        [EnumMember(Value = "canceled")] Canceled,
        [EnumMember(Value = "processing")] Processing,
        [EnumMember(Value = "action_required")] ActionRequired,
        [EnumMember(Value = "won")] Won,
        [EnumMember(Value = "lost")] Lost
    }
}