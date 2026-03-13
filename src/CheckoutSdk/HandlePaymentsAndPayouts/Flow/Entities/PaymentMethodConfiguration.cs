using Checkout.Payments;
using Newtonsoft.Json;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class PaymentMethodConfiguration
    {
        /// <summary>
        /// Configuration options specific to Apple Pay payments.
        /// </summary>
        [JsonProperty(PropertyName = "applepay")]
        public ApplePayConfiguration ApplePay { get; set; }

        /// <summary>
        /// Configuration options specific to card payments.
        /// </summary>
        public CardConfiguration Card { get; set; }

        /// <summary>
        /// Configuration options specific to Google Pay payments.
        /// </summary>
        [JsonProperty(PropertyName = "googlepay")]
        public GooglePayConfiguration GooglePay { get; set; }

        /// <summary>
        /// Configuration options specific to stored card payments.
        /// </summary>
        public StoredCardConfiguration StoredCard { get; set; }
    }
}