using System.Runtime.Serialization;

namespace Checkout.Tokens
{
    /// <summary>
    /// Defines the type of wallet.
    /// </summary>
    public enum TokenType
    {
        [EnumMember(Value = "applepay")] ApplePay,
        [EnumMember(Value = "googlepay")] GooglePay,
        [EnumMember(Value = "card")] Card
    }
}