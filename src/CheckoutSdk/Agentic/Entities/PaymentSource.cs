using Newtonsoft.Json;
using Checkout.Common;

namespace Checkout.Agentic.Entities
{
    /// <summary>
    /// Payment Source for Agentic Enrollment
    /// </summary>
    public class PaymentSource
    {
        /// <summary>
        /// Card number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Card expiry month (1-12)
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Card expiry year (e.g., 2025)
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Card verification value (CVV)
        /// </summary>
        public string Cvv { get; set; }

        /// <summary>
        /// Payment source type
        /// </summary>
        public PaymentSourceType? Type { get; set; }
    }
}