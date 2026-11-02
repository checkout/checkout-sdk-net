namespace Checkout.Payments.Request
{
    /// <summary>
    /// Configuration of asynchronous retries.
    /// </summary>
    [System.Obsolete("Use CustomerRetry for customer_retry, or RetryRequest for the retry of payments. " +
                     "This type will be removed in the next major version.")]
    public class PaymentRetryRequest
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment. Not part of customer_retry.
        /// [Required]
        /// </summary>
        [System.Obsolete("Not part of customer_retry; use RetryRequest.Enabled for payments. " +
                         "This member will be removed in the next major version.")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Configuration of asynchronous Dunning retries. Not part of customer_retry.
        /// [Optional]
        /// </summary>
        [System.Obsolete("Not part of customer_retry; use RetryRequest.Dunning for payments. " +
                         "This member will be removed in the next major version.")]
        public DunningRetryRequest Dunning { get; set; }

        /// <summary>
        /// Configuration of asynchronous Downtime retries. Not part of customer_retry.
        /// [Optional]
        /// </summary>
        [System.Obsolete("Not part of customer_retry; use RetryRequest.Downtime for payments. " +
                         "This member will be removed in the next major version.")]
        public DowntimeRetryRequest Downtime { get; set; }

        /// <summary>
        /// The maximum number of customer retry attempts. Use CustomerRetry.MaxAttempts instead.
        /// [Optional]
        /// </summary>
        [System.Obsolete("Use CustomerRetry.MaxAttempts instead. This member will be removed in the next major version.")]
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// Has no customer_retry equivalent and is ignored there.
        /// [Optional]
        /// </summary>
        [System.Obsolete("No customer_retry equivalent; ignored there. " +
                         "This member will be removed in the next major version.")]
        public int? EndAfterDays { get; set; }
    }
}
