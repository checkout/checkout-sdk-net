using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum AuthenticationType
    {
        [EnumMember(Value = "regular")] Regular,
        [EnumMember(Value = "recurring")] Recurring,
        [EnumMember(Value = "installment")] Installment,
        [EnumMember(Value = "maintain_card")] MaintainCard,
        [EnumMember(Value = "add_card")] AddCard,
    }
}