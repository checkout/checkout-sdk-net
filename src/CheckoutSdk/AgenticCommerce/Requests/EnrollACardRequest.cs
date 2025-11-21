using Checkout.AgenticCommerce.Common;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Enroll a card for use with agentic commerce
    /// </summary>
    public class EnrollACardRequest
    {
        /// <summary>
        /// The payment source to enroll
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// The user's device
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// The customer's details
        /// </summary>
        public Customer Customer { get; set; }
    }
}