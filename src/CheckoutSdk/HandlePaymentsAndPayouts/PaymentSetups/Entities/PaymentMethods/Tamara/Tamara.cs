namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Tamara payment method's details and configuration.
    /// </summary>
    public class Tamara : PaymentMethodBase
    {
        /// <summary>
        /// Risk signals about the customer, forwarded to Tamara with the payment.
        /// [Optional]
        /// </summary>
        public TamaraRiskAssessment RiskAssessment { get; set; }
    }
}
