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
        
        [Obsolete("This property will be removed in the future, and should not be used. Use AmountAllocations instead.", false)]
        public MarketplaceData Marketplace { get; set; }
        
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public Processing Processing { get; set; }

        public IDictionary<string, object> Metadata { get; set; }
    }
}