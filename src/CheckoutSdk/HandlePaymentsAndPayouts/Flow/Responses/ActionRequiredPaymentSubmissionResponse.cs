namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    /// <summary>
    /// Represents a payment submission response requiring further action (Status = "Action Required").
    /// All response properties are available on the base <see cref="PaymentSubmissionResponse"/> class.
    /// </summary>
    public class ActionRequiredPaymentSubmissionResponse : PaymentSubmissionResponse
    {
        public override string Status { get; set; } = "Action Required";
    }
}