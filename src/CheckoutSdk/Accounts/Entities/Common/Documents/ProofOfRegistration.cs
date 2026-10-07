namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Proof of the sole trader's registration, for example an extract from a trade register.
    /// Representative documents only (<c>company.representatives[].documents</c>), EEA Sole Trader
    /// Full (3.0); not accepted at the top level.
    /// </summary>
    public class ProofOfRegistration
    {
        /// <summary>
        /// The type of document being used as proof of registration.
        /// [Required]
        /// </summary>
        public ProofOfRegistrationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}