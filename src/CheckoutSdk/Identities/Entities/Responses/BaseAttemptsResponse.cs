using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for attempts responses
    /// </summary>
    public abstract class BaseAttemptsResponse<TAttemptData> : Resource
    {
        /// <summary>
        /// The total number of attempts
        /// [Required]
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of attempts skipped
        /// [Required]
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The maximum number of attempts returned
        /// [Required]
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The list of attempts
        /// [Required]
        /// </summary>
        public List<TAttemptData> Data { get; set; }
    }
}