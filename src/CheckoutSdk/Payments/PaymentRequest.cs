using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a request for a payment
    /// </summary>
    /// <typeparam name="TPaymentSource">The source of paymnt</typeparam>
    public class PaymentRequest<TPaymentSource> where TPaymentSource : IRequestSource
    {
        /// <summary>
        /// Creates a new payment request
        /// </summary>
        /// <param name="source">The source of the payment</param>
        /// <param name="currency">The three-letter ISO currency code</param>
        /// <param name="amount">The payment amount in the major currency. Omitting the amount or providing 0 will perform a card verification.</param>
        public PaymentRequest(TPaymentSource source, string currency, int? amount)
        {
            if (source == null)
                throw new ArgumentNullException("The payment source is required.", nameof(source));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("The currency is required.", nameof(currency));

            if (amount < 0)
                throw new ArgumentOutOfRangeException("The amount cannot be negative", nameof(amount));

            Source = source;
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Gets the source of payment
        /// </summary>
        public TPaymentSource Source { get; }

        /// <summary>
        /// Gets the payment amount in the major currency
        /// </summary>
        public int? Amount { get; }

        /// <summary>
        /// Gets the three-letter ISO currency code
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Gets or sets the payment type. 
        /// This must be specified for card payments where the cardholder is not present.
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// Gets or sets a reference you can later use to identify this payment such as an order number
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets a description of the payment
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to capture the payment automatically (default)
        /// </summary>
        public bool? Capture { get; set; }

        /// <summary>
        /// Gets or sets an ISO 8601 timestamp that determines when the payment should be captured. 
        /// Providing this field will automatically set capture to true.
        /// </summary>
        public DateTimeOffset? CaptureOn { get; set; }

        /// <summary>
        /// Gets or sets details of the customer associated with the payment
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets an optional dynamic billing descriptor displayed on the account owner's statement.
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the payment shipping details
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to process this payment as a 3D-Secure
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public bool ThreeDS { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to attempt a 3D-Secure payment as non-3DS should the card issuer not be enrolled.
        /// </summary>
        public bool? AttemptN3d { get; set; }

        /// <summary>
        /// Gets or sets the an existing payment identifier for payments that use stored card details such as recurring payments.
        /// </summary>
        public string PreviousPaymentId { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether risk checks for the requested payment should be skipped.
        /// </summary>
        public bool? SkipRiskCheck { get; set; }

        /// <summary>
        /// Gets or sets the success redirect URL overridding the default URL configured in the Checkout Hub
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Gets or sets the failure redirect URL overridding the default URL configured in the Checkout Hub
        /// </summary>
        public string FailureUrl { get; set; }

        /// <summary>
        /// Gets or sets the IP address used to make the payment. Required for some risk checks.
        /// </summary>
        public string PaymentIp { get; set; }

        /// <summary>
        /// Gets or sets the recipient of the payment. 
        /// Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions.
        /// </summary>
        public PaymentRecipient Recipient { get; set; }

        /// <summary>
        /// Gets or sets the metadata for the payment.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}