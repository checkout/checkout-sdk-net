using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards
{
    public enum CardStatus
    {
        [EnumMember(Value = "active")] Active,
        [EnumMember(Value = "inactive")] Inactive,
        [EnumMember(Value = "revoked")] Revoked,
        [EnumMember(Value = "suspended")] Suspended
    }
}