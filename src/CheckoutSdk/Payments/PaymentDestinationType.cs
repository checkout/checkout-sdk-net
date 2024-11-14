using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum PaymentDestinationType
    {
        [EnumMember(Value = "bank_account")] BankAccount,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "id")] Id,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "network_token")] NetworkToken
    }
}