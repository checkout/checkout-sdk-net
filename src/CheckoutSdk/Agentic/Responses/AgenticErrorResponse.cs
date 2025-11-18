namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Agentic Error Response launched when something went wrong
    /// </summary>
    public class AgenticErrorResponse : HttpMetadata
    {
        /// <summary>
        /// The request ID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// The error type if any
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// List of error codes if any
        /// </summary>
        public string[] ErrorCodes { get; set; }
    }
}