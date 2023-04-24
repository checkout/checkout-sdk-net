using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards
{
    public enum CardReasonType
    {
        [EnumMember(Value = "expired")] Expired,
        [EnumMember(Value = "reported_lost")] ReportedLost,

        [EnumMember(Value = "reported_stolen")]
        ReportedStolen,
        [EnumMember(Value = "suspected_lost")] SuspectedLost,

        [EnumMember(Value = "suspected_stolen")]
        SuspectedStolen,
    }
}