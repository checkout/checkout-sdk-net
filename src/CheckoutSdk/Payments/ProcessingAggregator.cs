namespace Checkout.Payments
{
    /// <summary>
    /// Information about the payment aggregator.
    /// </summary>
    public class ProcessingAggregator
    {
        /// <summary>
        /// The sub-merchant ID.
        /// </summary>
        public string SubMerchantId { get; set; }

        /// <summary>
        /// The Visa identifier for the payment aggregator.
        /// </summary>
        public string AggregatorIdVisa { get; set; }

        /// <summary>
        /// The Mastercard identifier for the payment aggregator.
        /// </summary>
        public string AggregatorIdMc { get; set; }
    }
}
