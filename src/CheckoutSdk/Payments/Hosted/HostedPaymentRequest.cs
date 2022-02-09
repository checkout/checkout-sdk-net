using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public BillingInformation Billing { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IList<Product> Products { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string CancelUrl { get; set; }

        public string FailureUrl { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string Locale { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("3ds")]
#else
        [JsonProperty(PropertyName = "3ds")]
#endif
        public ThreeDsRequest ThreeDs { get; set; }

        public bool Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string PaymentIp { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }
    }
}