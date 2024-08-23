using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Product = Checkout.Payments.Request.Product;

namespace Checkout.Payments.Sessions
{
    public class PaymentSessionsRequest
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public BillingInformation Billing { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public PaymentCustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public ProcessingSettings Processing { get; set; }

        public string ProcessingChannelId { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }
        
        public IList<PaymentMethodsType> EnabledPaymentMethods { get; set; }
        
        public IList<PaymentMethodsType> DisabledPaymentMethods { get; set; }

        public IList<Product> Items { get; set; }

        public IList<AmountAllocations> AmountAllocations { get; set; }

        public RiskRequest Risk { get; set; }

        public PaymentRetryRequest CustomerRetry { get; set; }

        public string DisplayName { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }
        
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public string Locale { get; set; }
        
        [JsonProperty(PropertyName = "3ds")] 
        public ThreeDsRequest ThreeDs { get; set; }
        
        public PaymentSender Sender { get; set; }
        
        public bool? Capture { get; set; }
        
        public DateTime? CaptureOn { get; set; }

        public string IpAddress { get; set; }

        public long? TaxAmount { get; set; }
    }
}