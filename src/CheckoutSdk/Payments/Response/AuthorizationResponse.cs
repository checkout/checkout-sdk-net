using Checkout.Common;
using System;

namespace Checkout.Payments.Response
{
    public class AuthorizationResponse : Resource
    {
        public string ActionId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public bool? Approved { get; set; }

        public PaymentStatus? Status { get; set; }

        public string AuthCode { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public PaymentResponseBalances Balances { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public string Reference { get; set; }

        public PaymentProcessing Processing { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }

        public RiskAssessment Risk { get; set; }
    }
}