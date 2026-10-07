namespace Checkout.Payments
{
    /// <summary>
    /// Returns the payment's risk assessment results.
    /// </summary>
    public class RiskAssessment
    {
        /// <summary>
        /// Whether or not the payment was flagged by a risk check.
        /// [Optional]
        /// Default: false
        /// </summary>
        public bool? Flagged { get; set; }

        /// <summary>
        /// The risk score calculated by our Fraud Detection engine. Absent if not enough data provided.
        /// [Optional]
        /// Decimal number, for example 22.5
        /// [ 0 .. 100 ]
        /// </summary>
        public double? Score { get; set; }
    }
}