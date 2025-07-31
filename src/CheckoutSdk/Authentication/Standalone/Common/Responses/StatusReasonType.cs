using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum StatusReasonType
    {
        [EnumMember(Value = "ares_status")]
        AresStatus,

        [EnumMember(Value = "ares_error")]
        AresError,

        [EnumMember(Value = "rreq_error")]
        RreqError,

        [EnumMember(Value = "rreq_status")]
        RreqStatus,

        [EnumMember(Value = "risk_declined")]
        RiskDeclined,
    }
}