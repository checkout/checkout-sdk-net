using System.Collections.Generic;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Agentic Configuration
    /// </summary>
    public class AgenticConfiguration
    {
        /// <summary>
        /// The AI model to use
        /// </summary>
        public string AiModel { get; set; }

        /// <summary>
        /// The maximum number of autonomous actions
        /// </summary>
        public int MaxAutonomousActions { get; set; }

        /// <summary>
        /// Whether to enable autonomous payments
        /// </summary>
        public bool EnableAutonomousPayments { get; set; }

        /// <summary>
        /// The risk threshold for autonomous actions
        /// </summary>
        public decimal RiskThreshold { get; set; }

        /// <summary>
        /// The allowed payment methods for autonomous transactions
        /// </summary>
        public List<string> AllowedPaymentMethods { get; set; }
    }
}