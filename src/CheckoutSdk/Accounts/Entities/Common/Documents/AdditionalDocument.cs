namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Additional space for documents to be provided when requested. Carries a file ID only; the
    /// API defines no document type for it.
    /// </summary>
    public class AdditionalDocument
    {
        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}