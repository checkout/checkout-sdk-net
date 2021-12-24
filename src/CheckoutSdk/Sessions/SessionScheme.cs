using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionScheme
    {
        [EnumMember(Value = "visa")] Visa,
        [EnumMember(Value = "mastercard")] Mastercard,
        [EnumMember(Value = "jcb")] Jcb,
        [EnumMember(Value = "amex")] Amex,
        [EnumMember(Value = "diners")] Diners
    }
}