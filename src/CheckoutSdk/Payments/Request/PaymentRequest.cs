using Checkout.Common;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PaymentRequest
    {
        /// <summary>
        /// Payment Context's unique identifier.
        /// [Optional]
        /// </summary>
        public string PaymentContextId { get; set; }

        /// <summary>
        /// The source of the payment.
        /// [Required]
        /// </summary>
        public AbstractRequestSource Source { get; set; }

        /// <summary>
        /// The payment amount. To perform a card verification, do not provide the amount or provide a value of 0.
        /// The amount must be provided in the minor currency unit.
        /// [Optional]
        /// >= 0
        /// &lt;= 9999999999
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code.
        /// [Optional]
        /// >= 3 characters
        /// &lt;= 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment. Required for card payments where the cardholder is not present (MOTO, MIT, recurring).
        /// For MITs, must not be set to Regular.
        /// [Optional]
        /// Enum: "Regular" "Recurring" "MOTO" "Installment" "PayLater" "Unscheduled"
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Payments.PaymentType.Regular;

        /// <summary>
        /// The details of a recurring subscription or installment.
        /// [Optional]
        /// </summary>
        public PaymentPlan PaymentPlan { get; set; }

        /// <summary>
        /// Whether the payment is a merchant-initiated transaction (MIT).
        /// Must be set to true for all MITs. If true, payment_type must not be Regular.
        /// [Optional]
        /// </summary>
        public bool? MerchantInitiated { get; set; }

        /// <summary>
        /// A reference you can use to identify the payment (e.g. an order number).
        /// [Optional]
        /// &lt;= 80 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment.
        /// [Optional]
        /// &lt;= 100 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The authorization type.
        /// [Optional]
        /// Enum: "Final" "Estimated"
        /// </summary>
        public AuthorizationType? AuthorizationType { get; set; }

        /// <summary>
        /// Partial authorization configuration for the payment.
        /// [Optional]
        /// </summary>
        public PartialAuthorization PartialAuthorization { get; set; }

        /// <summary>
        /// Whether to capture the payment (if applicable).
        /// [Optional]
        /// </summary>
        public bool? Capture { get; set; }

        /// <summary>
        /// A timestamp (ISO 8601) that determines when the payment should be captured.
        /// Providing this field will automatically set capture to true.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? CaptureOn { get; set; }

        /// <summary>
        /// The date and time when the payment expires in UTC (e.g. Multibanco payments).
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ExpireOn { get; set; }

        /// <summary>
        /// Customer information for the payment.
        /// [Optional]
        /// </summary>
        public CustomerRequest Customer { get; set; }

        /// <summary>
        /// Dynamic billing descriptor displayed on the customer's bank statement.
        /// [Optional]
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// Shipping details for the payment.
        /// [Optional]
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Provides information required to authenticate payments.
        /// [Optional]
        /// </summary>
        public Authentication Authentication { get; set; }

        /// <summary>
        /// Information required for 3D Secure authentication payments.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsRequest ThreeDs { get; set; }

        /// <summary>
        /// The processing channel to be used for the payment.
        /// [Optional]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// An identifier linking the payment to an existing recurring series.
        /// Only pass for MIT transactions in a recurring series.
        /// [Optional]
        /// &lt;= 100 characters
        /// </summary>
        public string PreviousPaymentId { get; set; }

        /// <summary>
        /// Risk assessment configuration for the payment.
        /// [Optional]
        /// </summary>
        public RiskRequest Risk { get; set; }

        /// <summary>
        /// For redirect payment methods, overrides the default success redirect URL configured on your account.
        /// [Optional]
        /// Format: uri
        /// &lt;= 1024 characters
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// For redirect payment methods, overrides the default failure redirect URL configured on your account.
        /// [Optional]
        /// Format: uri
        /// &lt;= 1024 characters
        /// </summary>
        public string FailureUrl { get; set; }

        /// <summary>
        /// The IP address used to make the payment. Only accepts IPv4 and IPv6 addresses.
        /// [Optional]
        /// </summary>
        [Obsolete("Use risk.device.network.ipv4 or risk.device.network.ipv6 instead.", false)]
        public string PaymentIp { get; set; }

        /// <summary>
        /// The sender of the payment.
        /// [Optional]
        /// </summary>
        public PaymentSender Sender { get; set; }

        /// <summary>
        /// The recipient of the payment.
        /// [Optional]
        /// </summary>
        public PaymentRecipient Recipient { get; set; }

        /// <summary>
        /// Marketplace data for the payment.
        /// [Optional]
        /// </summary>
        [Obsolete("This property will be removed in the future, and should not be used. Use AmountAllocations instead.", false)]
        public MarketplaceData Marketplace { get; set; }

        /// <summary>
        /// The amount allocations for marketplace payments.
        /// [Optional]
        /// </summary>
        public IList<AmountAllocations> AmountAllocations { get; set; }

        /// <summary>
        /// Additional payment processing settings.
        /// [Optional]
        /// </summary>
        public ProcessingSettings Processing { get; set; }

        /// <summary>
        /// The line items in the order associated with the payment.
        /// [Optional]
        /// </summary>
        public IList<Product> Items { get; set; }

        /// <summary>
        /// Retry configuration for the payment (Dunning and Downtime).
        /// [Optional]
        /// </summary>
        public RetryRequest Retry { get; set; }

        /// <summary>
        /// The details of the subscription linking this payment to a recurring series.
        /// [Optional]
        /// </summary>
        public PaymentSubscription Subscription { get; set; }

        /// <summary>
        /// Controls processor attempts at the payment level.
        /// [Optional]
        /// </summary>
        public PaymentRouting Routing { get; set; }

        /// <summary>
        /// Stores additional information about the transaction. Supports string, number, and boolean fields.
        /// Up to 20 fields per call; each value must not exceed 255 characters.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The segment associated with the payment.
        /// [Optional]
        /// </summary>
        public PaymentSegment Segment { get; set; }

        /// <summary>
        /// Details about the payment instruction.
        /// [Optional]
        /// </summary>
        public PaymentInstruction Instruction { get; set; }
    }
}
