namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// IRS-issued Employer Identification Number document used to verify the entity's tax
    /// identification (US variants).
    /// </summary>
    public class TaxVerification
    {
        /// <summary>
        /// The type of IRS-issued document used for tax verification.
        /// [Required]
        /// </summary>
        public TaxVerificationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}