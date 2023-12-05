using Checkout.Payments.Request.Source.Contexts;
using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsProcessing
    {
        public BillingPlan Plan { get; set; }

        public int ShippingAmount { get; set; }

        public string InvoiceId { get; set; }

        public string BrandName { get; set; }

        public string Locale { get; set; }

        public ShippingPreference? ShippingPreference { get; set; }

        public UserAction? UserAction { get; set; }

        public PaymentContextsPartnerCustomerRiskData PartnerCustomerRiskData { get; set; }

        public IList<PaymentContextsAirlineData> AirlineData { get; set; }
    }
}