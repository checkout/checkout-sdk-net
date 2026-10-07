namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// A document showing transactions from the last 3 months.
    /// </summary>
    public class BankVerification
    {
        /// <summary>
        /// The type of document being used as bank verification.
        /// [Required]
        /// </summary>
        public BankVerificationType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }
    }
}