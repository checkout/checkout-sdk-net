namespace Checkout.Payments.Request
{
    public class CancelAScheduledRetryRequest
    {
        /// <summary>
        /// A reference to identify the scheduled retry to cancel.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }
    }
}