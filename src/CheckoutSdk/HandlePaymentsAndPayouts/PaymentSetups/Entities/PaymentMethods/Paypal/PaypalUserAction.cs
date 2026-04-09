using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    public enum PaypalUserAction
    {
        [EnumMember(Value = "pay_now")]
        PayNow,
        [EnumMember(Value = "continue")]
        Continue
    }
}
