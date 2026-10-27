using System;

namespace Checkout.Payments.Response
{
    /// <summary>
    /// Configuration relating to asynchronous retries. Maps RetryPostResponse (POST /payments) and
    /// RetryGetResponse (GET /payments/{id}).
    /// </summary>
    public class PaymentRetryResponse
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment.
        /// [Required]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// The number of retries attempted so far. Only returned by GET /payments/{id}.
        /// [Optional] Minimum 0.
        /// </summary>
        public int? AttemptsMade { get; set; }

        /// <summary>
        /// The maximum number of authorization retry attempts, excluding the initial authorization.
        /// [Optional] Range [ 1 .. 15 ]. Default: 6
        /// </summary>
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// A timestamp that details the date on which the retry schedule expires, in ISO 8601 format.
        /// [Optional]
        /// </summary>
        public DateTime? EndsOn { get; set; }

        /// <summary>
        /// A timestamp that details the date on which the next authorization attempt will take place, in ISO 8601
        /// format.
        /// [Optional]
        /// </summary>
        public DateTime? NextAttemptOn { get; set; }
    }
}
