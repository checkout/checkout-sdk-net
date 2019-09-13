namespace Checkout.Payments
{
    /// <summary>
    /// Information related to the processing of a payment action
    /// </summary>
    public class ActionProcessingResponse : ProcessingResponse
    {
        /// <summary>
        /// Gets or sets the ARN generated during the processing of captures and refunds
        /// </summary>
        public string AcquirerReferenceNumber { get; set; }
    }
}