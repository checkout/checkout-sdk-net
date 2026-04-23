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
        [Newtonsoft.Json.JsonProperty(PropertyName = "aggregator_id_visa")]
        public string AggregatorIdVisa { get; set; }

        /// <summary>
        /// The Mastercard identifier for the payment aggregator.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "aggregator_id_mc")]
        public string AggregatorIdMc { get; set; }
    }
}
