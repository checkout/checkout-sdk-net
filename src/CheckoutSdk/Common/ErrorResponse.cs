using System.Collections.Generic;

namespace Checkout.Common
{
    /// <summary>
    /// Validation error response returned by Checkout APIs.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets a unique identifier for the request that can be provided to our support team to diagnose an issue.
        /// </summary>
        public string RequestId { get; set; }
        
        /// <summary>
        /// Gets or sets the type of error e.g. request_invalid.
        /// </summary>
        /// <value></value>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// Gets or sets the error codes that describe the validation error.
        /// </summary>
        public IEnumerable<string> ErrorCodes { get; set; }
    }
}