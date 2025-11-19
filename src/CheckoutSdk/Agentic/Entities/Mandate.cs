using System;

namespace Checkout.Agentic.Entities
{
    /// <summary>
    /// Mandate configuration for purchase intents
    /// </summary>
    public class Mandate
    {
        /// <summary>
        /// The mandate ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Purchase threshold configuration
        /// </summary>
        public PurchaseThreshold PurchaseThreshold { get; set; }

        /// <summary>
        /// Description of the mandate
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Expiration date of the mandate in ISO 8601 format
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}