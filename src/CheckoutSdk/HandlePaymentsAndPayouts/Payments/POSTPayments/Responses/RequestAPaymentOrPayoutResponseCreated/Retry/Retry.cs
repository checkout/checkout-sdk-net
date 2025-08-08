using System;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Retry
{
    /// <summary>
    /// retry
    /// Configuration relating to asynchronous retries
    /// </summary>
    public class Retry
    {
        /// <summary>
        /// Default:  6 The maximum number of authorization retry attempts, excluding the initial authorization.
        /// [ 1 .. 15 ]
        /// </summary>
        public int MaxAttempts { get; set; } = 6;

        /// <summary>
        /// A timestamp that details the date on which the retry schedule expires, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? EndsOn { get; set; }

        /// <summary>
        /// A timestamp of the date on which the next authorization attempt will take place, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? NextAttemptOn { get; set; }
    }
}