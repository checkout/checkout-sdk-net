#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System;

namespace Checkout.Disputes.Four
{
    public class PaymentDispute
    {
        public string Id { get; set; }

        public string ActionId { get; set; }

        public string ProcessingChannelId { get; set; }

        public int? Amount { get; set; }

        public string Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public string Mcc { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("3ds")]
#else
        [JsonProperty(PropertyName = "3ds")]
#endif
        public ThreeDsVersionEnrollment ThreeDs { get; set; }

        public string Eci { get; set; }

        public bool? HasRefund { get; set; }

        public DateTime? ProcessedOn { get; set; }
    }
}