using Checkout.Common;
using Checkout.Risk.source;
using System;
using System.Collections;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureAssessmentRequest
    {
        public string AssessmentId { get; set; }

        public DateTime? Date { get; set; }

        public RiskPaymentRequestSource Source { get; set; }

        public CustomerRequest Customer { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public RiskPayment Payment { get; set; }

        public RiskShippingDetails Shipping { get; set; }

        public Device Device { get; set; }

        public IDictionary Metadata { get; set; }

        public AuthenticationResult AuthenticationResult { get; set; }

        public AuthorizationResult AuthorizationResult { get; set; }
    }
}