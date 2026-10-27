using System;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Retry
{
    /// <summary>
    /// Configuration relating to asynchronous retries
    /// </summary>
    public class Retry
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment.
        /// [Required]
        /// </summary>
        public bool? Enabled { get; set; }

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
        /// A timestamp of the date on which the next authorization attempt will take place, in ISO 8601 format.
        /// [Optional]
        /// </summary>
        public DateTime? NextAttemptOn { get; set; }
    }
}