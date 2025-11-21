using System;

namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// Mandate configuration for purchase intents
    /// </summary>
    public class MandateExtended: MandateBase
    {
        /// <summary>
        /// The unique identifier for the mandate
        /// [Required]
        /// </summary>
        public string Id { get; set; }
    }
}