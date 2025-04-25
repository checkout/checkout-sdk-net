using Checkout.Common;
using System;

namespace Checkout.Issuing.Transactions.Responses
{
    public class Messages
    {
        public string Id { get; set; }
        
        public InitiatorType? Initiator { get; set; }
        
        public Type? Type { get; set; }
        
        public ResultType? Result { get; set; }
        
        public bool? IsRelayed { get; set; }
        
        public IndicatorType? Indicator { get; set; }
        
        public string DeclineReason { get; set; }
        
        public long? BillingAmount { get; set; }
        
        public Currency? BillingCurrency { get; set; }
        
        public DateTime? CreatedOn { get; set; }
    }
}