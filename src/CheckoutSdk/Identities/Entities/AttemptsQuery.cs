namespace Checkout.Identities.Entities
{
    /// <summary>
    /// Query parameters for retrieving the attempts made for a verification.
    /// </summary>
    public class AttemptsQuery
    {
        /// <summary>
        /// The number of attempts to skip.
        /// [Optional]
        /// Default: 0
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// The maximum number of attempts to return.
        /// [Optional]
        /// Default: 10
        /// </summary>
        public int? Limit { get; set; }
    }
}
