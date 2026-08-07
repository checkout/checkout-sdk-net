using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Response
{
    public class ProcessingData
    {
        /// <summary>
        /// The preferred scheme for co-badged card payment processing. If performing 3DS via a third
        /// party, set this value to the scheme that processed 3DS. This field does not support PINless
        /// debit schemes in the US (STAR, PULSE, NYCE, ACCEL, SHAZAM).
        /// [Optional]
        /// </summary>
        public PreferredSchema? PreferredScheme { get; set; }

        /// <summary>
        /// The customer's application identifier.
        /// [Optional]
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// The customer's ID on the partner platform.
        /// [Optional]
        /// </summary>
        public string PartnerCustomerId { get; set; }

        /// <summary>
        /// The partner-originated unique payment identifier.
        /// [Optional]
        /// </summary>
        public string PartnerPaymentId { get; set; }

        /// <summary>
        /// Total tax amount of the order.
        /// [Optional]
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// The country where the purchase was made.
        /// Not documented in the public spec for this response, kept for backward compatibility.
        /// [Optional]
        /// </summary>
        public CountryCode? PurchaseCountry { get; set; }

        /// <summary>
        /// The language and region of the customer. ISO 639-2 language code, its value consists of
        /// language-country.
        /// [Optional]
        /// ^[a-z]{2}(?:-[A-Z][a-z]{3})?(?:-(?:[A-Z]{2}))?$
        /// &gt;= 2 characters, &lt;= 10 characters
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// A unique identifier for the authorization provided by partner.
        /// [Optional]
        /// </summary>
        public string RetrievalReferenceNumber { get; set; }

        /// <summary>
        /// The Klarna order ID associated with the payment.
        /// [Optional]
        /// </summary>
        public string PartnerOrderId { get; set; }

        /// <summary>
        /// Status of a payment provided by partner.
        /// [Optional]
        /// </summary>
        public string PartnerStatus { get; set; }

        /// <summary>
        /// Unique transaction identification provided by partner.
        /// [Optional]
        /// </summary>
        public string PartnerTransactionId { get; set; }

        /// <summary>
        /// The list of error codes that led the payment to fail or be declined, as given by the
        /// payment provider.
        /// [Optional]
        /// </summary>
        public IList<string> PartnerErrorCodes { get; set; }

        /// <summary>
        /// Error description provided by partner.
        /// [Optional]
        /// </summary>
        public string PartnerErrorMessage { get; set; }

        /// <summary>
        /// Authorization code provided by partner.
        /// [Optional]
        /// </summary>
        public string PartnerAuthorizationCode { get; set; }

        /// <summary>
        /// Authorization response code provided by partner.
        /// [Optional]
        /// </summary>
        public string PartnerAuthorizationResponseCode { get; set; }

        /// <summary>
        /// Fraud status of the payment.
        /// Not documented in the public spec for this response, kept for backward compatibility.
        /// Prefer <see cref="PartnerFraudStatus"/>.
        /// [Optional]
        /// </summary>
        public string FraudStatus { get; set; }

        /// <summary>
        /// The payment method authorized by the provider.
        /// Not documented in the public spec for this response, kept for backward compatibility.
        /// [Optional]
        /// </summary>
        public ProviderAuthorizedPaymentMethod ProviderAuthorizedPaymentMethod { get; set; }

        /// <summary>
        /// An array defining which of the configured payment options within a payment category
        /// (for example, pay_later or pay_over_time) should be displayed for this purchase.
        /// [Optional]
        /// </summary>
        public IList<string> CustomPaymentMethodIds { get; set; }

        /// <summary>
        /// Indicates whether the payment is an Account Funding Transaction.
        /// [Optional]
        /// </summary>
        public bool? Aft { get; set; }

        /// <summary>
        /// Four-digit code for retail financial services expressed in ISO 18245 format, classifying
        /// the types of goods or services you provide.
        /// [Optional]
        /// </summary>
        public string MerchantCategoryCode { get; set; }

        /// <summary>
        /// The merchant identifier that was configured with the scheme and used for the payment.
        /// [Optional]
        /// </summary>
        public string SchemeMerchantId { get; set; }

        /// <summary>
        /// The type of Primary Account Number (PAN) used for the payment. DPAN indicates network
        /// token was used, FPAN indicates the full card was used.
        /// [Optional]
        /// </summary>
        public PanProcessedType? PanTypeProcessed { get; set; }

        /// <summary>
        /// The flag indicating if Checkout Network Token was available for the payment.
        /// Not documented in the public spec for this response, kept for backward compatibility.
        /// [Optional]
        /// </summary>
        public bool? CkoNetworkTokenAvailable { get; set; }

        /// <summary>
        /// Indicates whether the fallback_source field was used for the payment.
        /// [Optional]
        /// </summary>
        public bool? FallbackSourceUsed { get; set; }

        /// <summary>
        /// A high-level failure category returned by the payment provider when a payment is declined or fails.
        /// Not all payment methods return this field.
        /// [Optional]
        /// </summary>
        public string FailureCode { get; set; }

        /// <summary>
        /// The 6-digit partner code returned by the payment provider. Returned when source.type is blik.
        /// [Optional]
        /// Pattern: ^\d{6}$
        /// 6 characters
        /// </summary>
        public string PartnerCode { get; set; }

        /// <summary>
        /// The raw response code returned by the payment provider when a payment is declined or fails.
        /// Not all payment methods return this field.
        /// [Optional]
        /// </summary>
        public string PartnerResponseCode { get; set; }

        /// <summary>
        /// The scheme on which the payment was authorized. This may differ from the card's scheme used
        /// for the payment if the card is co-badged and the payment was authorized on a different network.
        /// [Optional] readOnly
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Partner fraud status. If the status is Pending, and the merchant captures before it changes
        /// to Accepted, the risk of the transaction is solely on the merchant.
        /// [Optional]
        /// </summary>
        public string PartnerFraudStatus { get; set; }

        /// <summary>
        /// The Mastercard Merchant Advice Code (MAC), which contains additional information about the
        /// transaction. For example, the MAC can inform you if the transaction was performed using a
        /// consumer non-reloadable prepaid card or a consumer single-use virtual card. For declined
        /// transactions, the MAC also indicates whether the payment can be retried and how long to wait.
        /// [Optional]
        /// </summary>
        public string PartnerMerchantAdviceCode { get; set; }

        /// <summary>
        /// Contains information about the accommodation booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<AccommodationData> AccommodationData { get; set; }

        /// <summary>
        /// Contains information about the airline ticket and flights booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<AirlineData> AirlineData { get; set; }

        /// <summary>
        /// The scheme transaction link identifier. Returned for Mastercard transactions when the scheme
        /// provides a link identifier that ties together related transactions on the network
        /// (see Mastercard Transaction Link Identifier documentation).
        /// [Optional]
        /// </summary>
        public string SchemeTransactionLinkId { get; set; }
    }
}
