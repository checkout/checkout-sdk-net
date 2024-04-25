using Checkout.Common;
using Checkout.Payments.Request.Destination;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Sender;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PayoutRequest 
    {
        public PayoutRequestSource Source { get; set; }

        public PaymentRequestDestination Destination { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public PayoutBillingDescriptor BillingDescriptor { get; set; }

        public PaymentSender Sender { get; set; }

        public PaymentInstruction Instruction { get; set; }

        public string ProcessingChannelId { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        
        public PaymentSegment Segment { get; set; }
               
    }
}