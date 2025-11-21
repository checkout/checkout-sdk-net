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
        public AgenticSource Source { get; set; }

        /// <summary>
        /// The user's device
        /// </summary>
        public AgenticDevice Device { get; set; }

        /// <summary>
        /// The customer's details
        /// </summary>
        public AgenticCustomer Customer { get; set; }
    }
}