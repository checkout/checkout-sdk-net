
using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Processing
    {
        /// <summary>
        /// Indicates whether the payment is an Account Funding Transaction
        /// </summary>
        public bool? Aft { get; set; }

        /// <summary>
        /// The discount amount applied to the transaction by the merchant.
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// The total freight or shipping and handling charges for the transaction.
        /// </summary>
        public decimal? ShippingAmount { get; set; }

        /// <summary>
        /// The customer's value-added tax registration number.
        /// </summary>
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Invoice ID number.
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The label that overrides the business name in the PayPal account on the PayPal pages.
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// The language and region of the customer in ISO 639-2 language code; value consists of language-country.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// An array of key-and-value pairs with merchant-specific data for the transaction.
        /// </summary>
        public IList<KeyValuePair<string, string>> PartnerCustomerRiskData { get; set; }

        /// <summary>
        /// Promo codes - An array that can be used to define which of the configured payment options 
        /// within a payment category (pay_later, pay_over_time, etc.) should be shown for this purchase.
        /// </summary>
        public IList<string> CustomPaymentMethodIds { get; set; }

        /// <summary>
        /// Contains information about the airline ticket and flights booked by the customer.
        /// </summary>
        public IList<AirlineData> AirlineData { get; set; }

        /// <summary>
        /// Contains information about the accommodation booked by the customer.
        /// </summary>
        public IList<AccommodationData> AccommodationData { get; set; }

        /// <summary>
        /// The number provided by the cardholder. Purchase order or invoice number may be used.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Surcharge amount applied to the transaction in minor units by the merchant.
        /// </summary>
        public long? SurchargeAmount { get; set; }

        /// <summary>
        /// The total charges for any import/export duty included in the transaction.
        /// </summary>
        public decimal? DutyAmount { get; set; }

        /// <summary>
        /// The tax amount of the freight or shipping and handling charges for the transaction.
        /// </summary>
        public decimal? ShippingTaxAmount { get; set; }

        /// <summary>
        /// The purchase country of the customer. ISO 3166 alpha-2 purchase country.
        /// </summary>
        public string PurchaseCountry { get; set; }

        /// <summary>
        /// Indicates the reason for a merchant-initiated payment request.
        /// </summary>
        public MerchantInitiatedReason? MerchantInitiatedReason { get; set; }

        /// <summary>
        /// Unique number of the campaign this payment will be running in. Only required for Afterpay campaign invoices.
        /// </summary>
        public int? CampaignId { get; set; }

        /// <summary>
        /// The payment for a merchant's order may be split, and the original order price indicates the transaction amount of the entire order.
        /// </summary>
        public decimal? OriginalOrderAmount { get; set; }

        /// <summary>
        /// Merchant receipt ID.
        /// </summary>
        public string ReceiptId { get; set; }

        /// <summary>
        /// A URL which you can use to notify the customer that the order has been created.
        /// </summary>
        public string MerchantCallbackUrl { get; set; }

        /// <summary>
        /// The line of business that the payment is associated with.
        /// </summary>
        public string LineOfBusiness { get; set; }

        /// <summary>
        /// Specifies the preferred type of Primary Account Number (PAN) for the payment
        /// </summary>
        public PanPreference? PanPreference { get; set; }

        /// <summary>
        /// Indicates whether to provision a network token for the payment. Default: true
        /// </summary>
        public bool? ProvisionNetworkToken { get; set; } = true;
    }

    public enum MerchantInitiatedReason
    {
        DelayedCharge,
        
        Resubmission,
        
        NoShow,
        
        Reauthorization
    }

    public enum PanPreference
    {
        Fpan,
        
        Dpan
    }
}