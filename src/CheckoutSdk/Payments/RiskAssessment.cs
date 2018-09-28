namespace Checkout.Payments
{
    /// <summary>
    /// Risk assessment results.
    /// </summary>
    public class RiskAssessment
    {
        /// <summary>
        /// Whether the payment was flagged by a risk check.
        /// </summary>
        public bool Flagged { get; set; }
    }
}