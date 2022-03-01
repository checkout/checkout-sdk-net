using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Links
{
    public class PaymentLinkDetailsResponse : Resource
    {
        public string Id { get; set; }

        public PaymentLinkStatus? Status { get; set; }

        public string PaymentId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public CustomerResponse Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public BillingInformation Billing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string ReturnUrl { get; set; }

        public string Locale { get; set; }

        // Only available in Four

        public string ProcessingChannelId { get; set; }

        public MarketplaceData Marketplace { get; set; }
    }
}