using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Update Agentic Response
    /// </summary>
    public class UpdateAgenticResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the agentic commerce
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the update operation
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Links for further actions
        /// </summary>
        public Dictionary<string, Link> Links { get; set; }
    }
}