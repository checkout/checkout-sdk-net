using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum StatusReason
    {
        [EnumMember(Value = "ares_error")] AresError,
        [EnumMember(Value = "ares_status")] AresStatus,
        [EnumMember(Value = "veres_error")] VeresError,
        [EnumMember(Value = "veres_status")] VeresStatus,
        [EnumMember(Value = "pares_error")] ParesError,
        [EnumMember(Value = "pares_status")] ParesStatus,
        [EnumMember(Value = "rreq_error")] RreqError,
        [EnumMember(Value = "rreq_status")] RreqStatus,
        [EnumMember(Value = "risk_declined")] RiskDeclined
    }
}