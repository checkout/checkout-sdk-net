using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as financial statements (US ISV Seller variants). Note the plural
    /// <c>financial_statements</c>; <see cref="FinancialVerificationType"/> is a different enum.
    /// </summary>
    public enum FinancialStatementsType
    {
        [EnumMember(Value = "financial_statements")]
        FinancialStatements
    }
}
