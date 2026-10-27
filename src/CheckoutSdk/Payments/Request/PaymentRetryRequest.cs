namespace Checkout.Payments.Request
{
    /// <summary>
    /// Configuration of asynchronous retries.
    /// </summary>
    [System.Obsolete("Use Checkout.Payments.RetryRequest for payment retries or " +
                     "Checkout.HandlePaymentsAndPayouts.Flow.Entities.CustomerRetry for customer_retry. " +
                     "This type will be removed in the next major version.")]
    public class PaymentRetryRequest
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment.
        /// [Required]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Configuration of asynchronous Dunning retries.
        /// [Optional]
        /// </summary>
        public DunningRetryRequest Dunning { get; set; }

        /// <summary>
        /// Configuration of asynchronous Downtime retries.
        /// [Optional]
        /// </summary>
        public DowntimeRetryRequest Downtime { get; set; }

        /// <summary>
        /// Not part of the retry configuration. Use <see cref="DunningRetryRequest.MaxAttempts"/> in
        /// <see cref="Dunning"/> instead.
        /// [Optional]
        /// </summary>
        [System.Obsolete("Use Dunning.MaxAttempts instead. This member will be removed in the next major version.")]
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// Not part of the retry configuration. Use <see cref="DunningRetryRequest.EndAfterDays"/> in
        /// <see cref="Dunning"/> instead.
        /// [Optional]
        /// </summary>
        [System.Obsolete("Use Dunning.EndAfterDays instead. This member will be removed in the next major version.")]
        public int? EndAfterDays { get; set; }
    }
}
