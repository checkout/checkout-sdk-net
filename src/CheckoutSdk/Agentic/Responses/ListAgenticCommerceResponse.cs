using System;
using System.Collections.Generic;
using Checkout.Agentic.Requests;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// List Agentic Commerce Response
    /// </summary>
    public class ListAgenticCommerceResponse : HttpMetadata
    {
        /// <summary>
        /// The list of agentic commerce items
        /// </summary>
        public List<AgenticCommerceItem> Items { get; set; }

        /// <summary>
        /// Total number of items available
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Number of items returned in this response
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Number of items skipped
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Maximum number of items requested
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Links for pagination and further actions
        /// </summary>
        public Dictionary<string, Link> Links { get; set; }
    }

    /// <summary>
    /// Agentic Commerce Item (summary view)
    /// </summary>
    public class AgenticCommerceItem
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

    /// <summary>
    /// Agentic Statistics Summary (condensed version)
    /// </summary>
    public class AgenticStatisticsSummary
    {
        /// <summary>
        /// Total number of autonomous actions performed
        /// </summary>
        public int TotalAutonomousActions { get; set; }

        /// <summary>
        /// Success rate of autonomous actions
        /// </summary>
        public decimal SuccessRate { get; set; }

        /// <summary>
        /// The timestamp of the last autonomous action
        /// </summary>
        public DateTime? LastAutonomousAction { get; set; }
    }
}