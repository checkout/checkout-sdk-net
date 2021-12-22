using Checkout.Common;
using Checkout.Risk.source;
using System;
using System.Collections;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationAssessmentRequest
    {
        public DateTime? Date { get; set; }

        public RiskPaymentRequestSource Source { get; set; }

        public CustomerRequest Customer { get; set; }

        public RiskPayment Payment { get; set; }

        public RiskShippingDetails Shipping { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public Device Device { get; set; }

        public IDictionary Metadata { get; set; }
    }
}