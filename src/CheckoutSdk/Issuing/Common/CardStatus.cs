using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    public enum CardStatus
    {
        [EnumMember(Value = "active")] Active,
        [EnumMember(Value = "inactive")] Inactive,
        [EnumMember(Value = "revoked")] Revoked,
        [EnumMember(Value = "suspended")] Suspended
    }
}