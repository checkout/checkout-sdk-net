namespace Checkout.Payments
{
    public class RetryRequest
    {
        /// <summary> Configuration of asynchronous Dunning retries (Optional) </summary>
        public Dunning Dunning { get; set; }

        /// <summary> Configuration of asynchronous Downtime retries (Optional) </summary>
        public Downtime Downtime { get; set; }
    }
}