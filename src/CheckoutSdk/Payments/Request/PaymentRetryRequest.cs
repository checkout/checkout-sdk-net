namespace Checkout.Payments.Request
{
    public class PaymentRetryRequest
    {
        /// <summary>
        /// Dunning retry configuration — retries after card declines.
        /// [Optional]
        /// </summary>
        public DunningRetryRequest Dunning { get; set; }

        /// <summary>
        /// Downtime retry configuration — retries during processor outages.
        /// [Optional]
        /// </summary>
        public DowntimeRetryRequest Downtime { get; set; }

        /// <summary>Use <see cref="Dunning"/> instead.</summary>
        [System.Obsolete("Use the nested Dunning object instead.")]
        public bool? Enabled { get; set; }

        /// <summary>Use <see cref="Dunning"/> instead.</summary>
        [System.Obsolete("Use the nested Dunning object instead.")]
        public int? MaxAttempts { get; set; }

        /// <summary>Use <see cref="Dunning"/> instead.</summary>
        [System.Obsolete("Use the nested Dunning object instead.")]
        public int? EndAfterDays { get; set; }
    }

    public class DunningRetryRequest
    {
        /// <summary>
        /// Whether dunning retries are enabled.
        /// [Optional]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// The maximum number of retry attempts.
        /// [Optional]
        /// </summary>
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// The number of days after which retries stop.
        /// [Optional]
        /// </summary>
        public int? EndAfterDays { get; set; }
    }

    public class DowntimeRetryRequest
    {
        /// <summary>
        /// Whether downtime retries are enabled.
        /// [Optional]
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
