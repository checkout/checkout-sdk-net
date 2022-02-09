using Checkout.Common;
using Checkout.Payments.Four.Response.Destination;
using Checkout.Payments.Four.Response.Source;
using Checkout.Payments.Four.Sender;
using Checkout.Payments.Four.Util;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
# endif
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Four.Response
{
    public class GetPaymentResponse : Resource
    {
        public string Id { get; set; }

        public DateTime? RequestedOn { get; set; }

#if NET5_0_OR_GREATER
        [JsonConverter(typeof(PaymentResponseSourceConverter))]
#else
        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
#endif
        public IResponseSource Source { get; set; }

#if NET5_0_OR_GREATER
        [JsonConverter(typeof(PaymentResponseDestinationConverter))]
#else
        [JsonConverter(typeof(PaymentResponseDestinationTypeConverter))]
#endif
        public IPaymentResponseDestination Destination { get; set; }

#if NET5_0_OR_GREATER
        [JsonConverter(typeof(PaymentResponseDestinationConverter))]
#else
        [JsonConverter(typeof(PaymentResponseDestinationTypeConverter))]
#endif
        public PaymentSender Sender { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public bool? Approved { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public PaymentStatus? Status { get; set; }

        public PaymentResponseBalances Balances { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("3ds")]
#else
        [JsonProperty(PropertyName = "3ds")]
#endif        
        public ThreeDsData ThreeDs { get; set; }

        public RiskAssessment Risk { get; set; }

        public CustomerResponse Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PaymentIp { get; set; }

        public MarketplaceData Marketplace { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }

        public IList<PaymentActionSummary> Actions { get; set; }
    }
}