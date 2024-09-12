using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum FinancialVerificationType
    {
        [EnumMember(Value = "financial_statement")]
        FinancialStatement
    }
}