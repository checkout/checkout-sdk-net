namespace Checkout.ComplianceRequests.Requests
{
    /// <summary>
    /// The payload used to respond to a compliance request.
    /// </summary>
    public class ComplianceRequestRespondRequest
    {
        /// <summary>
        /// The fields being responded to.
        /// [Required]
        /// </summary>
        public ComplianceRespondedFields Fields { get; set; }

        /// <summary>
        /// Optional free-text comment provided with the response.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string Comments { get; set; }
    }
}
