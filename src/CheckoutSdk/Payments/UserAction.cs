using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum UserAction
    {
        [EnumMember(Value = "PAY_NOW")] PayNow,
        [EnumMember(Value = "CONTINUE")] Continue,
    }
}