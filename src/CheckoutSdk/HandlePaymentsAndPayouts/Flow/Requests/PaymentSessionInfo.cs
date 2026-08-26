using Checkout.Payments.Sender;
using Checkout.Payments;
using Checkout.Common;

using Customer = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer;

using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Extended base class for payment session requests that include full payment details.
    ///
    /// This class holds only the properties that every payment session request accepts, whether it
    /// creates a session or submits a payment attempt for an existing one. Properties that only the
    /// session creation endpoints accept live in PaymentSessionCreateBase.
    /// </summary>
    public abstract class PaymentSessionInfo : PaymentSessionBase
    {
        /// <summary>
        /// The three-letter ISO currency code.
        /// Nullable so that a submit request omits it unless the caller sets it, which leaves the
        /// value provided when the payment session was created untouched.
        /// [Required] when creating a payment session
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The billing details.
        /// [Required] when creating a payment session
        /// </summary>
        public BillingInformation Billing { get; set; }

        /// <summary>
        /// Overrides the default success redirect URL configured on your account,
        /// for payment methods that require a redirect.
        /// [Required] when creating a payment session
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Overrides the default failure redirect URL configured on your account,
        /// for payment methods that require a redirect.
        /// [Required] when creating a payment session
        /// </summary>
        public string FailureUrl { get; set; }

        /// <summary>
        /// A description of the purchase, which is displayed on the customer's statement.
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// The customer's details. Required if source.type is tamara.
        /// </summary>
        public Customer.Customer Customer { get; set; }

        /// <summary>
        /// The shipping details.
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Information about the recipient of the payment's funds.
        /// </summary>
        public PaymentRecipient Recipient { get; set; }

        /// <summary>
        /// Use the processing object to influence or override the data sent during card processing.
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
        /// min 1 max 50 items
        /// </summary>
        public IList<AmountAllocations> AmountAllocations { get; set; }

        /// <summary>
        /// Allows you to store additional information about a transaction with custom fields.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// The sender of the payment.
        /// </summary>
        public PaymentSender Sender { get; set; }

        /// <summary>
        /// A timestamp specifying when to capture the payment, as an ISO 8601 code.
        /// If a value is provided, capture is automatically set to true by the API.
        /// </summary>
        public DateTime? CaptureOn { get; set; }
    }
}
