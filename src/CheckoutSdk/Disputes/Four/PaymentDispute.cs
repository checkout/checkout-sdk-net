using Newtonsoft.Json;
using System;

namespace Checkout.Disputes.Four
{
    public sealed class PaymentDispute
    {
        public string Id { get; set; }

        public string ActionId { get; set; }

        public string ProcessingChannelId { get; set; }

        public int? Amount { get; set; }

        public string Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public string Mcc { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsVersionEnrollment ThreeDs { get; set; }

        public string Eci { get; set; }

        public bool? HasRefund { get; set; }

        public DateTime? ProcessedOn { get; set; }
    }
}