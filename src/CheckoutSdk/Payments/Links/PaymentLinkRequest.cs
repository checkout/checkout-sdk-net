using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Product = Checkout.Common.Product;

namespace Checkout.Payments.Links
{
    /// <summary>
    /// Request to create a Payment Link.
    /// </summary>
    public class PaymentLinkRequest
    {
        /// <summary>
        /// The payment amount. The exact format depends on the currency.
        /// [Required]
        /// >= 1
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the payment. The currency and billing.address.country fields
        /// determine which payment methods are shown on the payment page.
        /// [Required]
        /// 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The billing details.
        /// [Required]
        /// </summary>
        public BillingInformation Billing { get; set; }

        /// <summary>
        /// This must be specified for card payments where the cardholder is not present (i.e., recurring or mail
        /// order / telephone order).
        /// [Optional]
        /// Enum: "Regular" "Recurring"
        /// Default: "Regular"
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// The Customers IP address. Only IPv4 and IPv6 addresses are accepted.
        /// [Optional]
        /// Deprecated
        /// </summary>
        [Obsolete("This property is deprecated in the API.", false)]
        public string PaymentIp { get; set; }

        /// <summary>
        /// An optional description that is displayed on the customer's statement identifying a purchase.
        /// [Optional]
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment.
        /// [Optional]
        /// &lt;= 100 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The merchant name to display to customers on the checkout page.
        /// [Optional]
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The processing channel to use for the payment. Required if you have more than one processing channel
        /// associated with your API key.
        /// [Optional]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The sub-entities that the payment is being processed on behalf of.
        /// [Optional]
        /// </summary>
        public IList<AmountAllocations> AmountAllocations { get; set; }

        /// <summary>
        /// The time for which the link remains valid, in seconds.
        /// [Optional]
        /// [ 1 .. 5184000 ]
        /// Default: 86400
        /// </summary>
        public int? ExpiresIn { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CustomerRequest Customer { get; set; }

        /// <summary>
        /// The address any products are being sent to.
        /// [Optional]
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Information about the recipient of the payment's funds.
        /// [Optional]
        /// </summary>
        public PaymentRecipient Recipient { get; set; }

        /// <summary>
        /// Use the processing object to influence or override the data sent during card processing.
        /// [Optional]
        /// </summary>
        public ProcessingSettings Processing { get; set; }

        /// <summary>
        /// Specifies which payment method options to present to the customer. The values in this field override
        /// any equivalent values in disabled_payment_methods.
        /// [Optional]
        /// </summary>
        public IList<PaymentSourceType> AllowPaymentMethods { get; set; }

        /// <summary>
        /// Specifies which payment method options to not present to the customer. If the same payment method is
        /// also in allow_payment_methods, this value is overridden.
        /// [Optional]
        /// </summary>
        public IList<PaymentSourceType> DisabledPaymentMethods { get; set; }

        /// <summary>
        /// Details about the products in the order.
        /// [Optional]
        /// </summary>
        public IList<Product> Products { get; set; }

        /// <summary>
        /// Allows you to store additional information about the transaction. This object only allows one level of
        /// depth, so cannot accept non-primitive data types such as objects or arrays.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Information required for 3D Secure payments.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsRequest ThreeDs { get; set; }

        /// <summary>
        /// Configures the risk assessment performed during payment processing.
        /// [Optional]
        /// </summary>
        public RiskRequest Risk { get; set; }

        /// <summary>
        /// Configures the parameters for customer retries. A customer retry is a payment attempt performed by the
        /// customer on the checkout page.
        /// [Optional]
        /// </summary>
        public CustomerRetry CustomerRetry { get; set; }

        /// <summary>
        /// The sender of the payment.
        /// [Optional]
        /// </summary>
        public PaymentSender Sender { get; set; }

        /// <summary>
        /// If provided, the success page will include a button that redirects your customer to the provided URL.
        /// [Optional]
        /// Format: uri
        /// &lt;= 255 characters
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Creates a translated version of the page in the specified language.
        /// [Optional]
        /// Default: "en-GB"
        /// </summary>
        public LocaleType? Locale { get; set; }

        /// <summary>
        /// Whether to capture the payment (if applicable).
        /// [Optional]
        /// </summary>
        public bool? Capture { get; set; }

        /// <summary>
        /// A timestamp (ISO 8601 code) that determines when the payment should be captured. Providing this field
        /// will automatically set capture to true.
        /// [Optional]
        /// </summary>
        public DateTime? CaptureOn { get; set; }

        /// <summary>
        /// Details about the payment instruction.
        /// [Optional]
        /// </summary>
        public PaymentInstruction Instruction { get; set; }

        /// <summary>
        /// Configuration options for specific payment methods.
        /// [Optional]
        /// </summary>
        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }

        /// <summary>
        /// The information to process a recurring payment request. To be used when the payment_type is Recurring.
        /// [Optional]
        /// </summary>
        public PaymentPlan PaymentPlan { get; set; }

        /// <summary>
        /// The authorization type.
        /// [Optional]
        /// Enum: "Final" "Estimated"
        /// Default: "Final"
        /// </summary>
        public AuthorizationType? AuthorizationType { get; set; }
    }
}
