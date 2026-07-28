namespace Checkout.Accounts.Entities.Common.Documents
{
    public class FinancialStatements
    {
        /// <summary>
        /// The type of document.
        /// </summary>
        public FinancialStatementsType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document.
        /// </summary>
        public string Front { get; set; }
    }
}
