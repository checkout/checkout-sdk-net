using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class Action : Resource
    {
        public string Id { get; set; }
        public ActionType Type { get; set; }
        public DateTime ProcessedOn { get; set; }
        public int Amount { get; set; }
        public string ResponseCode { get; set; }

        public string AuthCode { get; set; }
        public string Reference { get; set; }
        public string ResponseSummary { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}