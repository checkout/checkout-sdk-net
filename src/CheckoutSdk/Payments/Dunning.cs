namespace Checkout.Payments
{
    public class Dunning
    {
        /// <summary>
        /// Indicates if Checkout.com retries the payment when it's declined with a supported decline code (Required)
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary> (Optional, [ 1 .. 15 ], default: 6) </summary>
        public int? MaxAttempts { get; set; } = 6;

        /// <summary> (Optional, [ 1 .. 60 ] default: 30) </summary>
        public int? EndAfterDays { get; set; } = 30;
    }
}