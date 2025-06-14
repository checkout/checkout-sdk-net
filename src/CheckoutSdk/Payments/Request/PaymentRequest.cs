using Checkout.Common;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PaymentRequest
    {
        public string PaymentContextId { get; set; }
        
        public AbstractRequestSource Source { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; } = Payments.PaymentType.Regular;
        
        public PaymentPlan PaymentPlan { get; set; }

        public bool? MerchantInitiated { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }
        
        public Authentication Authentication { get; set; }

        public AuthorizationType? AuthorizationType { get; set; }

        public PartialAuthorization PartialAuthorization { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }
        
        public PaymentSegment Segment { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public string ProcessingChannelId { get; set; }

        public string PreviousPaymentId { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string PaymentIp { get; set; }

        public PaymentSender Sender { get; set; }

        public PaymentRecipient Recipient { get; set; }

        [Obsolete("This property will be removed in the future, and should not be used. Use AmountAllocations instead.", false)]
        public MarketplaceData Marketplace { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IList<Product> Items { get; set; }

        public RetryRequest Retry { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        
        public PaymentInstruction Instruction { get; set; }
    }
}
