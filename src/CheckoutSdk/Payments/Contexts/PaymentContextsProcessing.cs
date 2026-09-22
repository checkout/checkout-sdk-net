using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Settings that control how the payment context is processed.
    /// </summary>
    public class PaymentContextsProcessing
    {
        /// <summary>
        /// The plan details for a recurring payment with PayPal.
        /// Required when payment_type is recurring.
        /// [Optional]
        /// </summary>
        public BillingPlan Plan { get; set; }

        /// <summary>
        /// The discount amount the merchant applied to the transaction.
        /// [Optional]
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// The total freight or shipping and handling charges for the transaction.
        /// [Optional]
        /// </summary>
        public long? ShippingAmount { get; set; }

        /// <summary>
        /// The total tax amount for the transaction, in the minor currency unit.
        /// [Optional]
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// Invoice ID number.
        /// [Optional]
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The label that overrides the business name in the PayPal account on the PayPal pages.
        /// [Optional]
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// The language and region of the customer in ISO 639-2 language code; the value consists
        /// of language-country.
        /// [Optional]
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Shipping preference.
        /// [Optional]
        /// One of: no_shipping, set_provided_address, get_from_file
        /// </summary>
        public ShippingPreference? ShippingPreference { get; set; }

        /// <summary>
        /// Property required by PayPal to have an appropriate payment flow.
        /// [Optional]
        /// One of: pay_now, continue
        /// </summary>
        public UserAction? UserAction { get; set; }

        /// <summary>
        /// Key-and-value pairs with merchant-specific data for the transaction.
        /// [Optional]
        /// </summary>
        public IList<PartnerCustomerRiskData> PartnerCustomerRiskData { get; set; }

        /// <summary>
        /// Promo codes. Defines which of the configured payment options within a payment category
        /// (pay_later, pay_over_time, and so on) are shown for this purchase.
        /// [Optional]
        /// </summary>
        public IList<string> CustomPaymentMethodIds { get; set; }

        /// <summary>
        /// Contains information about the airline ticket and flights booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsAirlineData> AirlineData { get; set; }

        /// <summary>
        /// Contains information about the accommodation booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<AccommodationData> AccommodationData { get; set; }
    }
}
