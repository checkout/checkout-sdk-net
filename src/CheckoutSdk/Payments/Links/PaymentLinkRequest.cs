using Checkout.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Links
{
    public class PaymentLinkRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public int? ExpiresIn { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public BillingInformation Billing { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public RiskRequest Risk { get; set; }

        public string ReturnUrl { get; set; }

        public string Locale { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string PaymentIp { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public IList<PaymentSourceType> AllowPaymentMethods { get; set; }

        //Not available on Previous

        public string ProcessingChannelId { get; set; }

        [Obsolete("This property will be removed in the future, and should not be used. Use AmountAllocations instead.", false)]
        public MarketplaceData Marketplace { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }
    }
}