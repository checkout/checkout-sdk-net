using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Create Agentic Commerce Response
    /// </summary>
    public class CreateAgenticCommerceResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the created agentic commerce
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the agentic commerce
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The status of the agentic commerce
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Links for further actions
        /// </summary>
        public Dictionary<string, Link> Links { get; set; }
    }
}