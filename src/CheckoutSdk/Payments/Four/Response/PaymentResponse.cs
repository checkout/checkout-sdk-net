using Checkout.Common;
using Checkout.Payments.Four.Response.Source;
using Checkout.Payments.Four.Util;
using Newtonsoft.Json;
using System;

namespace Checkout.Payments.Four.Response
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

        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
        public IResponseSource Source { get; set; }

        public PaymentStatus? Status { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsEnrollment ThreeDs { get; set; }

        public string Reference { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public RiskAssessment Risk { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public PaymentResponseBalances Balances { get; set; }

        public PaymentProcessing Processing { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }
    }
}