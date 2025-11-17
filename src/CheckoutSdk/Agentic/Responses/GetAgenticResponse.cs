using System;
using System.Collections.Generic;
using Checkout.Agentic.Requests;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Get Agentic Response
    /// </summary>
    public class GetAgenticResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the agentic commerce
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the agentic commerce
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the agentic commerce
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The status of the agentic commerce
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Whether the agentic commerce is active
        /// </summary>
        public bool IsActive { get; set; }

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
        /// The timestamp when the agentic commerce was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Statistics about the agentic commerce performance
        /// </summary>
        public AgenticStatistics Statistics { get; set; }

        /// <summary>
        /// Links for further actions
        /// </summary>
        public Dictionary<string, Link> Links { get; set; }
    }
}