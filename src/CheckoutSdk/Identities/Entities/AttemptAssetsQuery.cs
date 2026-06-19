namespace Checkout.Identities.Entities
{
    /// <summary>
    /// Query parameters for retrieving the assets captured during an attempt.
    /// </summary>
    public class AttemptAssetsQuery
    {
        /// <summary>
        /// The number of assets to skip.
        /// [Optional]
        /// Default: 0
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// The maximum number of assets to return.
        /// [Optional]
        /// Default: 10
        /// </summary>
        public int? Limit { get; set; }
    }
}
