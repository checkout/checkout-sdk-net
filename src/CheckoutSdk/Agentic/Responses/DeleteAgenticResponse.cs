using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Delete Agentic Response
    /// </summary>
    public class DeleteAgenticResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the deleted agentic commerce
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the delete operation
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the agentic commerce was deleted
        /// </summary>
        public DateTime DeletedAt { get; set; }

        /// <summary>
        /// Message indicating the result of the delete operation
        /// </summary>
        public string Message { get; set; }
    }
}