using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class GetPaymentResponse : Resource
    {
        public string Id { get; set; }
        public DateTime RequestedOn { get; set; }
        [JsonConverter(typeof(SourceResponseConverter))]
        public IResponsePaymentSource Source { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsEnrollment ThreeDs { get; set; }
        public RiskResponse Risk { get; set; }
        public Customer Customer { get; set; }
        public BillingDescriptor BillingDescriptor { get; set; }
        public ShippingDetails Shipping { get; set; }
        public string PaymentIp { get; set; }
        public PaymentRecipient Recipient { get; set; }
        public IEnumerable<PaymentDestination> Destinations { get; set; }
        public Dictionary<string, object> Metadata { get; set; }

        public bool RequiresRedirect() => HasLink("redirect");
        public Link GetRedirectLink() => GetLink("redirect");
    }
}