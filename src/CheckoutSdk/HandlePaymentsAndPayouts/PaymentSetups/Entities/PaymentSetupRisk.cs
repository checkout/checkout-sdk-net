namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Risk assessment settings for the Payment Setup.
    /// </summary>
    public class PaymentSetupRisk
    {
        /// <summary>
        /// Indicates whether a risk assessment should be performed.
        /// [Optional]
        /// Default: true
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
