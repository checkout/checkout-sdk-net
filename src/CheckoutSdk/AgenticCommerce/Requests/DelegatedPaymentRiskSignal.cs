namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// A risk assessment signal provided by the platform to support fraud decisioning.
    /// </summary>
    public class DelegatedPaymentRiskSignal
    {
        /// <summary>
        /// The type of risk signal.
        /// [Required]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The risk score associated with this signal.
        /// [Required]
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The action taken based on the risk assessment.
        /// [Required]
        /// </summary>
        public string Action { get; set; }
    }
}
