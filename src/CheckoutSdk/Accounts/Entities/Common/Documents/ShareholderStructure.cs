namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Shareholder structure chart (including % of shares) certified by a competent authority
    /// individual and dated within the last 3 months.
    /// </summary>
    public class ShareholderStructure
    {
        /// <summary>
        /// The type of document.
        /// [Required]
        /// </summary>
        public ShareholderStructureType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}