using Checkout.Payments.Sender;
using Checkout.Payments;
using Checkout.Common;

using Customer = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer;

using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Extended base class for payment session requests that include full payment details
    /// </summary>
    public abstract class PaymentSessionInfo : PaymentSessionBase
    {
        /// <summary>
        /// The three-letter ISO currency code
        /// [Required]
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The billing details.
        /// [Required]
        /// </summary>
        public BillingInformation Billing { get; set; }

        /// <summary>
        /// Overrides the default success redirect URL configured on your account, 
        /// for payment methods that require a redirect.
        /// [Required]
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Overrides the default failure redirect URL configured on your account, 
        /// for payment methods that require a redirect.
        /// [Required]
        /// </summary>
        public string FailureUrl { get; set; }

        /// <summary>
        /// A description of the purchase, which is displayed on the customer's statement.
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// A description for the payment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The customer's details. Required if source.type is tamara.
        /// </summary>
        public Customer.Customer Customer { get; set; }

        /// <summary>
        /// The shipping details
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Information about the recipient of the payment's funds.
        /// </summary>
        public PaymentRecipient Recipient { get; set; }

        /// <summary>
        /// Use the processing object to influence or override the data sent during card processing
        /// </summary>
        public ProcessingSettings Processing { get; set; }

        /// <summary>
        /// Details about the payment instruction.
        /// </summary>
        public PaymentInstruction Instruction { get; set; }

        /// <summary>
        /// The processing channel to use for the payment.
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The sub-entities that the payment is being processed on behalf of.
        /// </summary>
        public IList<AmountAllocations> AmountAllocations { get; set; }

        /// <summary>
        /// Configures the risk assessment performed during payment processing.
        /// </summary>
        public RiskRequest Risk { get; set; }

        /// <summary>
        /// The merchant's display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Allows you to store additional information about a transaction with custom fields.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Creates a translated version of the page in the specified language. Default: "en-GB"
        /// </summary>
        public LocaleType? Locale { get; set; } = LocaleType.EnGb;

        /// <summary>
        /// The sender of the payment.
        /// </summary>
        public PaymentSender Sender { get; set; }

        /// <summary>
        /// Specifies whether to capture the payment, if applicable. Default: true
        /// </summary>
        public bool? Capture { get; set; } = true;

        /// <summary>
        /// A timestamp specifying when to capture the payment, as an ISO 8601 code.
        /// </summary>
        public DateTime? CaptureOn { get; set; }
    }
}