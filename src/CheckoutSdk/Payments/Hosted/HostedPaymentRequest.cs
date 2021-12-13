using Checkout.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
    public sealed class HostedPaymentRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails ShippingDetails { get; set; }

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

        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsRequest ThreeDS { get; set; }

        public bool Capture { get; set; }

        public DateTime? CaptureOn { get; set; }
    }
}