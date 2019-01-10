using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines a source response.
    /// </summary>
    public class SourceResponse : Resource
    {
        /// <summary>
        /// Gets or sets the processed response returned following a successfully processed source (HTTP Status Code 201).
        /// </summary>
        public SourceProcessed Source { get; set; }

        /// <summary>
        /// Gets or sets the pending response returned for asynchronous source or when further action such as a redirect is required (HTTP Status Code 202).
        /// </summary>
        public SourcePending Pending { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the source is in a pending state.
        /// </summary>
        public bool IsPending => Pending != null;

        /// <summary>
        /// Enables the implicit conversion of <see cref="SourcePending"/> to <see cref="SourceResponse"/>.
        /// This is required for dynamic dispatch during the deserialization of source responses.
        /// </summary>
        /// <param name="pendingResponse">The pending response.</param>
        public static implicit operator SourceResponse(SourcePending pendingResponse)
        {
            return new SourceResponse { Pending = pendingResponse };
        }

        /// <summary>
        /// Enables the implicit conversion of <see cref="SourceProcessed"/> to <see cref="SourceResponse"/>.
        /// This is required for dynamic dispatch during the deserialization of source responses.
        /// </summary>
        /// <param name="processedResponse">The processed response.</param>
        public static implicit operator SourceResponse(SourceProcessed processedResponse)
        {
            return new SourceResponse { Source = processedResponse };
        }
    }
}
