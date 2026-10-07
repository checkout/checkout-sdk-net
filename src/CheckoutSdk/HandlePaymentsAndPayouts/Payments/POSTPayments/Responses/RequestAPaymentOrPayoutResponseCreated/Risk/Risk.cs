namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Risk
{
    /// <summary>
    /// risk
    /// Returns the payment's risk assessment results
    /// </summary>
    public class Risk
    {
        /// <summary>
        /// Default:  false Whether or not the payment was flagged by a risk check
        /// [Optional]
        /// </summary>
        public bool? Flagged { get; set; } = false;

        /// <summary>
        /// The risk score calculated by our Fraud Detection engine. Absent if not enough data provided.
        /// [Optional]
        /// Decimal number, for example 22.5
        /// [ 0 .. 100 ]
        /// </summary>
        public double? Score { get; set; }
    }
}