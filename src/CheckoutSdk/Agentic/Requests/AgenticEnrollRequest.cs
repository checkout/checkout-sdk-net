using Checkout.Common;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Agentic Enroll Request
    /// </summary>
    public class AgenticEnrollRequest
    {
        /// <summary>
        /// Payment source information
        /// </summary>
        public PaymentSource Source { get; set; }

        /// <summary>
        /// Device information for fraud detection and analysis
        /// </summary>
        public DeviceInfo Device { get; set; }

        /// <summary>
        /// Customer information
        /// </summary>
        public CustomerRequest Customer { get; set; }
    }
}