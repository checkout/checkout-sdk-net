namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Risk signals about the customer, forwarded to Tamara with the payment.
    /// </summary>
    public class TamaraRiskAssessment
    {
        /// <summary>
        /// Specifies whether the customer is a premium customer.
        /// [Optional]
        /// </summary>
        public bool? PremiumCustomer { get; set; }

        /// <summary>
        /// The customer's risk score.
        /// [Optional]
        /// </summary>
        public int? RiskScore { get; set; }

        /// <summary>
        /// The number of orders the customer has made in the last 24 hours.
        /// [Optional]
        /// </summary>
        public int? OrderCountLast24h { get; set; }

        /// <summary>
        /// The number of devices linked to the customer.
        /// [Optional]
        /// </summary>
        public int? LinkedDeviceCount { get; set; }
    }
}
