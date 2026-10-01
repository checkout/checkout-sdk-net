namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// Not defined by any Accounts API onboarding schema. Referenced only by
    /// <see cref="Company.Document"/> and the obsolete <c>FinancialDocuments</c>.
    /// </summary>
    public class EntityDocument
    {
        /// <summary>
        /// Not defined by any Accounts API onboarding schema.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Not defined by any Accounts API onboarding schema.
        /// </summary>
        public string FileId { get; set; }
    }
}