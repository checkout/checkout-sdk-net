namespace Checkout.Payments.Request
{
    /// <summary>
    /// Configuration of Dunning retries.
    /// </summary>
    [System.Obsolete("Use Checkout.Payments.Dunning instead. This type will be removed in the next major version.")]
    public class DunningRetryRequest
    {
        /// <summary>
        /// Indicates if Checkout.com retries the payment when it's declined with a supported decline code.
        /// [Required]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// The maximum number of authorization retry attempts, excluding the initial authorization.
        /// [Optional] Range [ 1 .. 15 ]. Default: 6
        /// </summary>
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// The maximum number of days between the initial request and the final retry attempt.
        /// [Optional] Range [ 1 .. 60 ]. Default: 30
        /// </summary>
        public int? EndAfterDays { get; set; }
    }
}
