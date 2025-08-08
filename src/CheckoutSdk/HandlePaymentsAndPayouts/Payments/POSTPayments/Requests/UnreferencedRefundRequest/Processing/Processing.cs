namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Processing
{
    /// <summary>
    /// processing
    /// Returns information related to the processing of the payment.
    /// </summary>
    public class Processing
    {
        /// <summary>
        /// The speed at which the unreferenced refund is processed.
        /// Only applicable for unreferenced refunds.
        /// [Optional]
        /// </summary>
        public string ProcessingSpeed { get; set; }
    }
}