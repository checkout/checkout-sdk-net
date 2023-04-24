using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards.Requests.Suspend

{
    public enum SuspendReason
    {
        [EnumMember(Value = "suspected_lost")] SuspectedLost,
        [EnumMember(Value = "suspected_stolen")] SuspectedStolen
    }
}