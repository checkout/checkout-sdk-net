using Checkout.Common;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Util;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System;

namespace Checkout.Payments.Response
{
    public class PaymentResponse : Resource
    {
        public string ActionId { get; set; }

        public long? Amount { get; set; }

        public bool? Approved { get; set; }

        public string AuthCode { get; set; }

        public string Id { get; set; }

        public Currency? Currency { get; set; }

        public CustomerResponse Customer { get; set; }

#if NET5_0_OR_GREATER
        [JsonConverter(typeof(PaymentResponseSourceConverter))]
#else
        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]

#endif
        public IResponseSource Source { get; set; }

        public PaymentStatus? Status { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("3ds")]
#else
        [JsonProperty(PropertyName = "3ds")]
#endif
        public ThreeDsEnrollment ThreeDs { get; set; }

        public string Reference { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public RiskAssessment Risk { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public PaymentProcessing Processing { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }
    }
}