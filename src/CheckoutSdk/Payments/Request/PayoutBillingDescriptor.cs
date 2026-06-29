namespace Checkout.Payments.Request
{
    public class PayoutBillingDescriptor
    {
        /// <summary>
        /// The reference shown on the beneficiary's bank statement.
        /// Format: The length varies per beneficiary bank; fewer than 11 characters recommended.
        /// References are not supported in the US.
        /// [Required]
        /// min 11 characters
        /// max 15 characters
        /// </summary>
        public string Reference { get; set; }
    }
}