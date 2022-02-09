using Checkout.Common;
using Checkout.Payments.Request.Source;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PaymentRequest 
    {
        public AbstractRequestSource Source { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public bool? MerchantInitiated { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PreviousPaymentId { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string PaymentIp { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("3ds")]
#else
        [JsonProperty(PropertyName = "3ds")]
#endif  
        public ThreeDsRequest ThreeDs { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public IDictionary<string, object> Processing { get; set; } = new Dictionary<string, object>();
            
    }
}