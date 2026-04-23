namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    /// <summary>
    /// Represents an approved payment submission response (Status = "Approved").
    /// All response properties are available on the base <see cref="PaymentSubmissionResponse"/> class.
    /// </summary>
    public class ApprovedPaymentSubmissionResponse : PaymentSubmissionResponse
    {
        /// <summary>
        /// The payment status. Always "Approved" for this response type.
        /// </summary>
        public override string Status { get; set; } = "Approved";
    }
}