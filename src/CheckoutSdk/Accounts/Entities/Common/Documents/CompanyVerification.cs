namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document to use to confirm the company's identity (certified by a power of attorney
    /// within the last 3 months).
    /// </summary>
    public class CompanyVerification
    {
        /// <summary>
        /// The type of document used for company verification. <c>articles_of_association</c> is
        /// accepted on the US Company (2.0) variants only.
        /// [Required]
        /// </summary>
        public CompanyVerificationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}