using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as financial verification. Note the singular
    /// <c>financial_statement</c>; <see cref="FinancialStatementsType"/> is a different enum.
    /// </summary>
    public enum FinancialVerificationType
    {
        [EnumMember(Value = "financial_statement")]
        FinancialStatement
    }
}