using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentDetailsResponse : Resource
    {
        public string Id { get; set; }

        public HostedPaymentStatus? Status { get; set; }

        public string PaymentId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public CustomerResponse Customer { get; set; }

        public BillingInformation Billing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string SuccessUrl { get; set; }

        public string CancelUrl { get; set; }

        public string FailureUrl { get; set; }

        public string Locale { get; set; }
    }
}