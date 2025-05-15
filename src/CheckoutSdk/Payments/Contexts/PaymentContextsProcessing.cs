using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsProcessing
    {
        public BillingPlan Plan { get; set; }

        public long? DiscountAmount { get; set; }

        public long? ShippingAmount { get; set; }
        
        public long? TaxAmount { get; set; }

        public string InvoiceId { get; set; }

        public string BrandName { get; set; }

        public string Locale { get; set; }

        public ShippingPreference? ShippingPreference { get; set; }

        public UserAction? UserAction { get; set; }

        public PartnerCustomerRiskData PartnerCustomerRiskData { get; set; }
        
        public IList<string> CustomPaymentMethodIds { get; set; }

        public IList<PaymentContextsAirlineData> AirlineData { get; set; }
        
        public IList<AccommodationData> AccommodationData { get; set; }
    }
}