using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum AuthenticationType
    {
        [EnumMember(Value = "add_card")] AddCard,
        [EnumMember(Value = "installment")] Installment,
        [EnumMember(Value = "maintain_card")] MaintainCard,
        [EnumMember(Value = "recurring")] Recurring,
        [EnumMember(Value = "regular")] Regular
    }
}