namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Memorandum or Articles of Association document.
    /// </summary>
    public class ArticlesOfAssociation
    {
        /// <summary>
        /// The type of document used.
        /// [Required]
        /// </summary>
        public ArticlesOfAssociationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}