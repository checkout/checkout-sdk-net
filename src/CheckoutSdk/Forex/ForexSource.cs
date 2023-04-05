using System.Runtime.Serialization;

namespace Checkout.Forex
{
    public enum ForexSource
    {
        [EnumMember(Value = "visa")] Visa,
        [EnumMember(Value = "mastercard")] Mastercard,
    }
}