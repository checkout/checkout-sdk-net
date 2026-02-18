using Newtonsoft.Json;

namespace Checkout.PaymentMethods.Requests
{
    public class PaymentMethodsQueryFilter
    {
        /// <summary>
        /// The processing channel to be used for payment methods retrieval
        /// [Required]
        /// </summary>
        [JsonProperty(PropertyName = "processing_channel_id")]
        public string ProcessingChannelId { get; set; }
    }
}