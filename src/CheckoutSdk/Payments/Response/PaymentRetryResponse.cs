using System;

namespace Checkout.Payments.Response
{
    public class PaymentRetryResponse
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment.
        /// [Optional]
        /// </summary>
        public bool? Enabled { get; set; }

        public int? AttemptsMade { get; set; }

        public int? MaxAttempts { get; set; }

        public DateTime EndsOn { get; set; }

        public DateTime NextAttemptOn { get; set; }
    }
}