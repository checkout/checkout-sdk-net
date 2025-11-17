using System.Collections.Generic;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Create Agentic Commerce Request
    /// </summary>
    public class CreateAgenticCommerceRequest
    {
        /// <summary>
        /// The name of the agentic commerce
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the agentic commerce
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The configuration settings for the agentic commerce
        /// </summary>
        public AgenticConfiguration Configuration { get; set; }

        /// <summary>
        /// The metadata associated with the agentic commerce
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// The webhook endpoints for the agentic commerce
        /// </summary>
        public List<string> WebhookEndpoints { get; set; }
    }

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