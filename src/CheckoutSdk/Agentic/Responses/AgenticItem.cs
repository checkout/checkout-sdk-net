using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Agentic Item (summary view)
    /// </summary>
    public class AgenticItem
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
        /// The AI model being used
        /// </summary>
        public string AiModel { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Basic statistics summary
        /// </summary>
        public AgenticStatisticsSummary StatisticsSummary { get; set; }

        /// <summary>
        /// Links for further actions on this specific item
        /// </summary>
        public Dictionary<string, Link> Links { get; set; }
    }
}