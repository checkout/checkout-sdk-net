using Checkout.Common;
using System.Collections.Generic;
using Product = Checkout.Payments.Four.Request.Product;

namespace Checkout.Payments.Four
{
    public class CaptureRequest
    {
        public long? Amount { get; set; }

        public CaptureType? CaptureType { get; set; }

        public string Reference { get; set; }

        public PaymentCustomerRequest Customer { get; set; }

        public string Description { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public IList<Product> Items { get; set; }

        public MarketplaceData Marketplace { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}