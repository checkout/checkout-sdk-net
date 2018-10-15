namespace Checkout.Payments
{
    /// <summary>
    /// Configures the risk assessment performed during the processing of the payment
    /// </summary>
    public class RiskRequest
    {
        /// <summary>
        /// Creates a new <see cref="RiskRequest"/> instance.
        /// </summary>
        /// <param name="enabled">Whether a risk assessment should be performed.</param>
        public RiskRequest(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets whether the risk assessment should be performed.
        /// </summary>
        public bool Enabled { get; }
    }
}