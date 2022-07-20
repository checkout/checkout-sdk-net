using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class PaymentAction : Resource
    {
        public string Id { get; set; }

        public ActionType? Type { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public long? Amount { get; set; }

        public bool? Approved { get; set; }

        public string AuthCode { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public AuthorizationType? AuthorizationType { get; set; }

        public string Reference { get; set; }

        public Processing Processing { get; set; }

        public IDictionary<string, object> Metadata { get; set; }
    }
}