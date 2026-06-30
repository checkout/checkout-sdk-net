namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// Fraud-related information required when the dispute reason code is fraud-related.
    /// [Beta]
    /// </summary>
    public class IssuingDisputeFraudDetails
    {
        /// <summary>
        /// The type of fraud the cardholder is asserting.
        /// [Required]
        /// </summary>
        public IssuingDisputeFraudType? FraudType { get; set; }

        /// <summary>
        /// A description of the fraud circumstances, for internal reference.
        /// This is not forwarded to the scheme.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }
    }
}
