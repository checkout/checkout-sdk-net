using Checkout.Common;

namespace Checkout.Payments.Previous.Response.Source
{
    public abstract class AbstractResponseSource
    {
        public PaymentSourceType? Type { get; set; }

        public string Id { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
    }
}