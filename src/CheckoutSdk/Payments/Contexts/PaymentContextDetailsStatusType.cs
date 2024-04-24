using System.Runtime.Serialization;

namespace Checkout.Payments.Contexts
{
    public enum PaymentContextDetailsStatusType
    {
        [EnumMember(Value = "Created")] Created,
        [EnumMember(Value = "Approved")] Approved
    }
}