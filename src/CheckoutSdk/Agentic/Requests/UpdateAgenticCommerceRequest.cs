using System.Collections.Generic;

namespace Checkout.Agentic.Requests
{
    /// <summary>
    /// Update Agentic Commerce Request
    /// </summary>
    public class UpdateAgenticCommerceRequest
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

        /// <summary>
        /// Whether the agentic commerce is active
        /// </summary>
        public bool? IsActive { get; set; }
    }
}