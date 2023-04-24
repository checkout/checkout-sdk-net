using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards.Requests.Revoke
{
    public enum RevokeReason
    {
        [EnumMember(Value = "expired")] Expired,
        [EnumMember(Value = "reported_lost")] ReportedLost,
        [EnumMember(Value = "reported_stolen")] ReportedStolen,
    }
}