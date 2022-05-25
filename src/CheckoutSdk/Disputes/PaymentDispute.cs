using Checkout.Common;
using Newtonsoft.Json;
using System;

namespace Checkout.Disputes
{
    public class PaymentDispute
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public DateTime? ProcessedOn { get; set; }
        
        //Only available in Four
        
        public string ActionId { get; set; }
        
        public string ProcessingChannelId { get; set; }
        
        public string Mcc { get; set; }

        [JsonProperty(PropertyName = "3ds")] 
        public ThreeDsVersionEnrollment ThreeDs { get; set; }

        public string Eci { get; set; }

        public bool? HasRefund { get; set; }

    }
}