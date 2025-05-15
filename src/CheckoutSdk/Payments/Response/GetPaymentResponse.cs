using Checkout.Common;
using Checkout.Payments.Response.Destination;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Sender;
using Checkout.Payments.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Product = Checkout.Common.Product;

namespace Checkout.Payments.Response
{
    public class GetPaymentResponse : Resource
    {
        public string Id { get; set; }

        public DateTime? RequestedOn { get; set; }

        public string ProcessedOn { get; set; }
        
        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
        public IResponseSource Source { get; set; }

        [JsonConverter(typeof(PaymentResponseDestinationTypeConverter))]
        public IPaymentResponseDestination Destination { get; set; }

        public long? Amount { get; set; }
        
        public long? AmountRequested { get; set; }
        
        [JsonConverter(typeof(PaymentResponseSenderTypeConverter))]
        public ISender Sender { get; set; }
        
        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public PaymentPlan PaymentPlan { get; set; }

        public string Reference { get; set; }
        
        public string Description { get; set; }
        
        public bool? Approved { get; set; }
        
        public DateTime? ExpiresOn { get; set; }
        
        public PaymentStatus? Status { get; set; }
        
        public PaymentResponseBalances Balances { get; set; }
        
        
        [JsonProperty(PropertyName = "3ds")] 
        public ThreeDsData ThreeDs { get; set; }

        public RiskAssessment Risk { get; set; }

        public CustomerResponse Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PaymentIp { get; set; }

        [Obsolete("This property will be removed in the future, and should not be used. Use AmountAllocations instead.", false)]
        public MarketplaceData Marketplace { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public PaymentRecipient Recipient { get; set; }
        
        public ProcessingData Processing { get; set; }
        
        public IList<Product> Items { get; set; }
        
        public IDictionary<string, object> Metadata { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }

        public IList<PaymentActionSummary> Actions { get; set; }
        
        public PaymentRetryResponse Retry { get; set; }
        
        public PanProcessedType? PanTypeProcessed { get; set; }
        
        public bool? CkoNetworkTokenAvailable { get; set; }
        
        public PaymentInstruction Instruction { get; set; }
        

    }
}