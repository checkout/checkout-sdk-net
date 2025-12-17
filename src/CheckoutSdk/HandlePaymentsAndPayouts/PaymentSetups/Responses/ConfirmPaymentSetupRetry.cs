using System;

namespace Checkout.Payments.Setups
{
    /// <summary>
    /// [Beta]
    /// </summary>
    public class ConfirmPaymentSetupRetry
    {
        /// <summary>
        /// The maximum number of authorization retry attempts, excluding the initial authorization
        /// [ 1 .. 15 ]
        /// [Optional]
        /// </summary>
        public int? MaxAttempts { get; set; } = 6;

        /// <summary>
        /// A timestamp that details the date on which the retry schedule expires, in ISO 8601 format
        /// [Optional]
        /// </summary>
        public DateTime? EndsOn { get; set; }

        /// <summary>
        /// A timestamp of the date on which the next authorization attempt will take place, in ISO 8601 format
        /// [Optional]
        /// </summary>
        public DateTime? NextAttemptOn { get; set; }
    }
}