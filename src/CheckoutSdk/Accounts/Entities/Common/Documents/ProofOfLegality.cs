namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// A regulatory licence document required for the company to operate (when applicable).
    /// </summary>
    public class ProofOfLegality
    {
        /// <summary>
        /// The type of document used for proof of legality.
        /// [Required]
        /// </summary>
        public ProofOfLegalityType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}