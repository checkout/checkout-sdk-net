using System.Collections.Generic;
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
}