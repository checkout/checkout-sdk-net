namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document to use to confirm the individual's identity.
    /// </summary>
    public class IdentityVerification
    {
        /// <summary>
        /// The type of document used for identity verification.
        /// [Required]
        /// </summary>
        public IdentityVerificationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }

        /// <summary>
        /// The ID of the back side of the document as represented within Checkout.com systems.
        /// [Optional]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Back { get; set; }
    }
}