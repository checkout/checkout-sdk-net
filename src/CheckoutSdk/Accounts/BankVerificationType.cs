using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum BankVerificationType
    {
        [EnumMember(Value = "bank_statement")] BankStatement
    }
}