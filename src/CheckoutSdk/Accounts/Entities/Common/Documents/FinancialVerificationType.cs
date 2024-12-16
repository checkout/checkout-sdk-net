using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum FinancialVerificationType
    {
        [EnumMember(Value = "financial_statement")]
        FinancialStatement
    }
}