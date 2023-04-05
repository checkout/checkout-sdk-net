using Checkout.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string PaymentIp { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

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

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public bool Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public IList<PaymentSourceType> AllowPaymentMethods { get; set; }

        //Not available on Previous

        public string ProcessingChannelId { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }
    }
}