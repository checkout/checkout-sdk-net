namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Proof of residential address of the representative. Representative documents only
    /// (<c>company.representatives[].documents</c>), EEA Sole Trader Full (3.0); not accepted at the
    /// top level.
    /// </summary>
    public class ProofOfResidentialAddress
    {
        /// <summary>
        /// The type of document being used as address verification.
        /// [Required]
        /// </summary>
        public ProofOfResidentialAddressType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}