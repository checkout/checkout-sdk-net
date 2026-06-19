using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for paginated attempt assets responses.
    /// </summary>
    /// <typeparam name="TAssetData">the type of asset returned in the data list</typeparam>
    public abstract class BaseAttemptAssetsResponse<TAssetData> : Resource
    {
        /// <summary>
        /// The total number of assets.
        /// [Required]
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of assets skipped.
        /// [Required]
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The maximum number of assets returned.
        /// [Required]
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The list of assets for the current page.
        /// [Required]
        /// </summary>
        public List<TAssetData> Data { get; set; }
    }
}
