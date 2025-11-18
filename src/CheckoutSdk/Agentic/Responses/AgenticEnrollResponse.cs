using System;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Agentic Enroll Response
    /// </summary>
    public class AgenticEnrollResponse : HttpMetadata
    {
        /// <summary>
        /// The unique token identifier for the enrolled agentic service
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// Current status of the enrollment (e.g., "enrolled")
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the enrollment was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}