using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    public class PaymentSessionCompleteRequest
    {
        /// <summary>
        /// A unique token representing the additional customer data captured by Flow, 
        /// as received from the handleSubmit callback.
        /// Do not log or store this value.
        /// </summary>
        public string SessionData { get; set; }

        /// <summary>
        /// The payment amount. Provide a value of 0 to perform a card verification.
        /// The amount must be provided in the minor currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The billing details.
        /// </summary>
        public BillingDetails Billing { get; set; }

        /// <summary>
        /// Overrides the default success redirect URL configured on your account, 
        /// for payment methods that require a redirect.
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Overrides the default failure redirect URL configured on your account, 
        /// for payment methods that require a redirect.
        /// </summary>
        public string FailureUrl { get; set; }

        /// <summary>
        /// Must be specified for card-not-present (CNP) payments. Default: "Regular"
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Entities.PaymentType.Regular;

        /// <summary>
        /// A description of the purchase, which is displayed on the customer's statement.
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description for the payment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The customer's details. Required if source.type is tamara.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The shipping details
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Information about the recipient of the payment's funds.
        /// </summary>
        public Recipient Recipient { get; set; }

        /// <summary>
        /// Use the processing object to influence or override the data sent during card processing
        /// </summary>
        public Processing Processing { get; set; }

        /// <summary>
        /// Details about the payment instruction.
        /// </summary>
        public Instruction Instruction { get; set; }

        /// <summary>
        /// The processing channel to use for the payment.
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The line items in the order.
        /// </summary>
        public IList<Item> Items { get; set; }

        /// <summary>
        /// The sub-entities that the payment is being processed on behalf of.
        /// </summary>
        public IList<AmountAllocation> AmountAllocations { get; set; }

        /// <summary>
        /// Configures the risk assessment performed during payment processing.
        /// </summary>
        public Entities.Risk Risk { get; set; }

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
        public Locale? Locale { get; set; } = Entities.Locale.EnGB;

        /// <summary>
        /// Information required for 3D Secure authentication payments.
        /// </summary>
        public ThreeDSRequest ThreeDS { get; set; }

        /// <summary>
        /// The sender of the payment.
        /// </summary>
        public AbstractSender Sender { get; set; }

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