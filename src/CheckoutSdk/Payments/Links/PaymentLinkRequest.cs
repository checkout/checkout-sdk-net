using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Product = Checkout.Common.Product;

namespace Checkout.Payments.Links
{
    public class PaymentLinkRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }
        
        public BillingInformation Billing { get; set; }
        
        public PaymentType? PaymentType { get; set; }
        
        public string PaymentIp { get; set; }
        
        public BillingDescriptor BillingDescriptor { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }
        
        public string DisplayName { get; set; }
        
        public string ProcessingChannelId { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public int? ExpiresIn { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public ProcessingSettings Processing { get; set; }
        
        public IList<PaymentSourceType> AllowPaymentMethods { get; set; }
        
        public IList<PaymentSourceType> DisabledPaymentMethods { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public RiskRequest Risk { get; set; }

        public PaymentRetryRequest CustomerRetry { get; set; }

        public PaymentSender Sender { get; set; }

        public string ReturnUrl { get; set; }

        public LocaleType Locale { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }
        
        public PaymentInstruction Instruction { get; set; }

        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }
    }
}