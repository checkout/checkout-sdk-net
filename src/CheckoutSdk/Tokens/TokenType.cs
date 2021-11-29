using System.Runtime.Serialization;

namespace Checkout.Tokens
{
    public enum TokenType
    {
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "applepay")] ApplePay,
        [EnumMember(Value = "googlepay")] GooglePay
    }
}