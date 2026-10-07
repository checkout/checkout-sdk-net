namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Proof of the company's principal place of business.
    /// </summary>
    public class ProofOfPrincipalAddress
    {
        /// <summary>
        /// The type of document being used as address verification.
        /// [Required]
        /// </summary>
        public ProofOfPrincipalAddressType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}