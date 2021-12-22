using Checkout.Common;
using Checkout.Payments.Four.Request.Destination;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Sender;

namespace Checkout.Payments.Four.Request
{
    public sealed class PayoutRequest 
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
               
    }
}