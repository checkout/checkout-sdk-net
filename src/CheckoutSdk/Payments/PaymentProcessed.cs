using System;
using Checkout.Sdk.Common;
using Newtonsoft.Json;

namespace Checkout.Sdk.Payments
{
    public class PaymentProcessed<TSource> : Resource
    {
        public string Id { get; set; }
        public string ActionId { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }
        public string AuthCode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseSummary { get; set; }
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsEnrollment ThreeDs { get; set; }
        public RiskResponse Risk { get; set; }
        public TSource Source { get; set; }
        public Customer Customer { get; set; }
        public DateTime ProcessedOn { get; set; }

        public Link GetActionsLink() => GetLink("actions");
        public bool CanCapture() => HasLink("capture");
        public Link GetCaptureLink() => GetLink("capture");
        public bool CanVoid() => HasLink("void");
        public Link GetVoidLink() => GetLink("void");
    }
}