namespace Checkout.Payments
{
    public class RetryRequest
    {
        /// <summary>
        /// Indicates whether asynchronous retries are enabled for the payment.
        /// [Optional]
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary> Configuration of asynchronous Dunning retries (Optional) </summary>
        public Dunning Dunning { get; set; }

        /// <summary> Configuration of asynchronous Downtime retries (Optional) </summary>
        public Downtime Downtime { get; set; }
    }
}