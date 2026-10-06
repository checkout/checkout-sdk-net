namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Financial statement document. Becomes mandatory depending on the answer provided for
    /// <c>annual_processing_volume</c>; the sub-entity's status changes to <c>requirements_due</c>
    /// when it is needed.
    /// </summary>
    public class FinancialVerification
    {
        /// <summary>
        /// The type of the file.
        /// [Required]
        /// </summary>
        public FinancialVerificationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}