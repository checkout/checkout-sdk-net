using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum BankVerificationType
    {
        [EnumMember(Value = "bank_statement")] BankStatement
    }
}