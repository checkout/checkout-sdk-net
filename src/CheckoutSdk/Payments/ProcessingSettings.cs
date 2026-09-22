using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Settings that control how the payment is processed.
    /// </summary>
    /// <remarks>
    /// Shared across several request shapes. <c>POST /payments</c> resolves to
    /// <c>PaymentRequestProcessing</c>, while hosted payments, payment links and payment
    /// sessions resolve to the wider <c>PaymentInterfacesProcessing</c>. A property is therefore
    /// not necessarily read by every endpoint that accepts this object; the remarks below name
    /// the exceptions.
    /// </remarks>
    public class ProcessingSettings
    {
        /// <summary>
        /// Indicates if the payment is an Account Funding Transaction.
        /// [Optional]
        /// </summary>
        public bool? Aft { get; set; }

        /// <summary>
        /// The discount amount applied to the transaction by the merchant.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// The total freight or shipping and handling charges for the transaction.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? ShippingAmount { get; set; }

        /// <summary>
        /// The total amount of sales tax on the total purchase amount.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// Invoice ID number.
        /// [Optional]
        /// max 127 characters
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The label that overrides the business name in the PayPal account on the PayPal pages.
        /// [Optional]
        /// max 127 characters
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// The language and region of the customer in ISO 639-2 language code; the value consists
        /// of language-country.
        /// [Optional]
        /// Pattern: ^[a-z]{2}(?:-[A-Z][a-z]{3})?(?:-(?:[A-Z]{2}))?$
        /// 2 to 10 characters
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Key-and-value pairs with merchant-specific data for the transaction.
        /// [Optional]
        /// </summary>
        public PartnerCustomerRiskData PartnerCustomerRiskData { get; set; }

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
        public IList<AirlineData> AirlineData { get; set; }

        /// <summary>
        /// Contains information about the accommodation booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<AccommodationData> AccommodationData { get; set; }

        /// <summary>
        /// The number provided by the cardholder. A purchase order or invoice number may be used.
        /// [Optional]
        /// max 15 characters
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Surcharge amount applied to the transaction by the merchant, in the minor currency unit.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? SurchargeAmount { get; set; }

        /// <summary>
        /// The total charges for any import or export duty included in the transaction.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? DutyAmount { get; set; }

        /// <summary>
        /// The tax amount of the freight or shipping and handling charges for the transaction.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? ShippingTaxAmount { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the purchase country.
        /// [Optional]
        /// max 2 characters
        /// </summary>
        public CountryCode? PurchaseCountry { get; set; }

        /// <summary>
        /// Indicates the reason for a merchant-initiated payment request.
        /// [Optional]
        /// One of: Delayed_charge, Resubmission, No_show, Reauthorization
        /// </summary>
        public MerchantInitiatedReason? MerchantInitiatedReason { get; set; }

        /// <summary>
        /// Unique number of the campaign this payment runs in. Only required for Afterpay
        /// campaign invoices.
        /// [Optional]
        /// </summary>
        public long? CampaignId { get; set; }

        /// <summary>
        /// The payment for a merchant's order may be split; the original order price indicates
        /// the transaction amount of the entire order.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? OriginalOrderAmount { get; set; }

        /// <summary>
        /// Merchant receipt ID.
        /// [Optional]
        /// max 32 characters
        /// </summary>
        public string ReceiptId { get; set; }

        /// <summary>
        /// A URL you can use to notify the customer that the order has been created.
        /// [Optional]
        /// </summary>
        public string MerchantCallbackUrl { get; set; }

        /// <summary>
        /// The line of business for the payment. Beta.
        /// [Optional]
        /// </summary>
        public string LineOfBusiness { get; set; }

        /// <summary>
        /// Specifies the preferred type of Primary Account Number (PAN) for the payment.
        /// Only applies when source.type is a card, instrument or token.
        /// [Optional]
        /// One of: fpan, dpan
        /// </summary>
        public PanProcessedType? PanPreference { get; set; }

        /// <summary>
        /// Indicates whether to provision a network token for the payment.
        /// [Optional]
        /// </summary>
        public bool? ProvisionNetworkToken { get; set; }

        /// <summary>
        /// The preferred scheme for co-badged card payment processing. If performing 3DS through
        /// a third party, set this to the scheme that processed 3DS.
        /// [Optional]
        /// One of: mastercard, visa, cartes_bancaires
        /// </summary>
        public PreferredSchema? PreferredScheme { get; set; }

        /// <summary>
        /// Product type of the payment. Required when source.type is wechatpay.
        /// [Optional]
        /// </summary>
        public ProductType? ProductType { get; set; }

        /// <summary>
        /// Value obtained from the WeChat Web Authorization API before initiating Official
        /// Account or Mini Program payments. Required if source.type is wechatpay.
        /// [Optional]
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// The client-side terminal type: a website opened in a desktop browser, a mobile
        /// browser, or a mobile application.
        /// [Optional]
        /// One of: APP, WAP, WEB
        /// </summary>
        public TerminalType? TerminalType { get; set; }

        /// <summary>
        /// The operating system type. Required when TerminalType is not WEB.
        /// [Optional]
        /// One of: ANDROID, IOS
        /// </summary>
        public OsType? OsType { get; set; }

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
        /// Not in the current specification.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Serializes as <c>set_transaction_context</c>, which no request processing schema
        /// defines, so the gateway discards it. Retained for backwards compatibility.
        /// </remarks>
        public IList<IDictionary<string, string>> SetTransactionContext { get; set; }

        /// <summary>
        /// Not in the current specification.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Serializes as <c>dlocal</c>, which no request processing schema defines, so the
        /// gateway discards it. Retained for backwards compatibility.
        /// </remarks>
        public DLocalProcessingSettings Dlocal { get; set; }

        /// <summary>
        /// One time password sent to the customer by SMS.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        /// <remarks>
        /// Defined on the payment contexts payment request and on the capture request, not on
        /// <c>PaymentRequestProcessing</c>.
        /// </remarks>
        public string OtpValue { get; set; }

        /// <summary>
        /// Not in the current specification.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Serializes as <c>shipping_delay</c>, which no request processing schema defines, so
        /// the gateway discards it. Retained for backwards compatibility.
        /// </remarks>
        public long? ShippingDelay { get; set; }

        /// <summary>
        /// Not in the current specification.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Serializes as <c>shipping_info</c>, which no request processing schema defines, so the
        /// gateway discards it. Retained for backwards compatibility.
        /// </remarks>
        public IList<ShippingInfo> ShippingInfo { get; set; }

        /// <summary>
        /// Not in the current specification.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Serializes as <c>senderInformation</c>, in camelCase, which no request processing
        /// schema defines, so the gateway discards it. Retained for backwards compatibility.
        /// </remarks>
        [JsonProperty(PropertyName = "senderInformation")]
        public string SenderInformation { get; set; }

        /// <summary>
        /// Specifies whether to process the payment as a credit or debit transaction, if a combo
        /// card is used. Required for domestic payments in Brazil.
        /// [Optional]
        /// One of: credit, debit
        /// </summary>
        public CardType? CardType { get; set; }

        /// <summary>
        /// The unique identifier for Visa-registered ramp providers. Required if you are a
        /// Visa-registered ramp provider operating with affiliates.
        /// [Optional]
        /// Pattern: ^[a-zA-Z0-9]{1,15}$
        /// max 15 characters
        /// </summary>
        public string AffiliateId { get; set; }

        /// <summary>
        /// The affiliate URL. Required if you are a Visa-registered ramp provider operating with
        /// affiliates.
        /// [Optional]
        /// </summary>
        public string AffiliateUrl { get; set; }

        /// <summary>
        /// Information about the payment aggregator.
        /// [Optional]
        /// </summary>
        public ProcessingAggregator Aggregator { get; set; }

        /// <summary>
        /// The transaction identifier to track a payment request.
        /// [Optional]
        /// </summary>
        public string ReconciliationId { get; set; }

        /// <summary>
        /// The foreign retailer amount the merchant applied to the transaction, in the minor
        /// currency unit.
        /// [Optional]
        /// minimum 0
        /// </summary>
        public long? ForeignRetailerAmount { get; set; }

        /// <summary>
        /// Specifies which ACH service to use for the payment, if you set source.type to ach.
        /// [Optional]
        /// One of: same_day, standard
        /// </summary>
        public AchServiceType? ServiceType { get; set; }

        /// <summary>
        /// The customer's 6-digit Blik code. Required when source.type is blik and
        /// merchant_initiated is false (for example, for Regular payments and the initial payment
        /// of a Recurring agreement).
        /// [Optional]
        /// Pattern: ^\d{6}$
        /// 6 characters
        /// </summary>
        public string PartnerCode { get; set; }

        /// <summary>
        /// The scheme transaction link identifier.
        /// [Optional]
        /// </summary>
        public string SchemeTransactionLinkId { get; set; }
    }
}
