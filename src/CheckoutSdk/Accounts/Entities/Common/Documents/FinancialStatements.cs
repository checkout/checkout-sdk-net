namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Audited or management-prepared financial statements (when applicable). US ISV Seller
    /// variants only. Not the same document as <see cref="FinancialVerification"/>, whose type is
    /// the singular <c>financial_statement</c>.
    /// </summary>
    public class FinancialStatements
    {
        /// <summary>
        /// The type of document.
        /// [Required]
        /// </summary>
        public FinancialStatementsType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}