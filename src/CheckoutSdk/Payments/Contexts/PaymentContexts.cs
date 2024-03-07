using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    public class PaymentContexts
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string AuthorizationType { get; set; }
        
        public bool? Capture { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public PaymentContextsProcessing Processing { get; set; }

        public string ProcessingChannelId { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public IList<PaymentContextsItems> Items { get; set; }
    }
}