using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Setups.Entities;

namespace Checkout.Payments.Setups.Responses
{
    /// <summary>
    /// A Payment Setup, as returned when you create, update or get it.
    /// [Beta]
    /// </summary>
    public class PaymentSetupsResponse : Resource
    {
        /// <summary>
        /// The unique identifier of the Payment Setup.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The processing channel to use for the payment.
        /// [Required]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The payment amount, in the minor currency unit:
        /// https://www.checkout.com/docs/payments/accept-payments/format-the-amount-value
        /// [Required]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The currency of the payment, as a three-letter ISO currency code:
        /// https://www.checkout.com/docs/developer-resources/codes/currency-codes
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment.
        /// You must provide this field for card payments in which the cardholder is not present. For example, if the
        /// transaction is a recurring payment, or a mail order/telephone order (MOTO) payment.
        /// [Optional]
        /// Default: Regular
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Payments.PaymentType.Regular;

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number.
        /// [Optional]
        /// max 80 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment.
        /// [Optional]
        /// max 100 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The payment methods that are enabled on your account and available for use.
        /// [Optional]
        /// </summary>
        public Entities.PaymentMethods PaymentMethods { get; set; }

        /// <summary>
        /// Settings for the Payment Setup.
        /// [Optional]
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The customer's order details.
        /// [Optional]
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Industry-specific information, including airline and accommodation data.
        /// [Optional]
        /// </summary>
        public Industry Industry { get; set; }

        /// <summary>
        /// The billing details for the payment.
        /// [Optional]
        /// </summary>
        public Billing Billing { get; set; }

        /// <summary>
        /// An ordered list of available payment method names. The order indicates the recommended
        /// presentation priority, with the first item being the highest priority.
        /// [Optional]
        /// readOnly
        /// </summary>
        public IList<string> AvailablePaymentMethods { get; set; }

        /// <summary>
        /// Account funding transaction details for the payment.
        /// [Optional]
        /// </summary>
        public PaymentSetupAccountFundingTransaction AccountFundingTransaction { get; set; }

        /// <summary>
        /// The billing descriptor for the payment.
        /// [Optional]
        /// </summary>
        public PaymentSetupBillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// The latest payment for this setup. Returned after a successful confirmation, including auto-confirm
        /// during setup creation. For the full response schema, see Get payment details.
        /// [Optional]
        /// readOnly
        /// </summary>
        public object LatestPayment { get; set; }

        /// <summary>
        /// The amount and currency to present to the customer, when the settlement currency differs from the
        /// customer-facing currency.
        /// [Optional]
        /// </summary>
        public PaymentSetupPresentmentDetails PresentmentDetails { get; set; }

        /// <summary>
        /// Terminal details.
        /// [Optional]
        /// </summary>
        public PaymentSetupTerminal Terminal { get; set; }

        /// <summary>
        /// Risk assessment settings for the Payment Setup.
        /// [Optional]
        /// </summary>
        public PaymentSetupRisk Risk { get; set; }

        /// <summary>
        /// A set of key/value pairs you can attach to a payment. It can be useful for storing additional
        /// information about the payment. Each value is a string (max 255 characters), a number or a boolean.
        /// [Optional]
        /// max 20 properties
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }
    }
}
