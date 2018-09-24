using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentRequest<TSource> where TSource : IPaymentSource
    {
        /// <summary>
        /// Payment request
        /// </summary>
        /// <param name="source">The source of the payment</param>
        /// <param name="currency">The three-letter ISO currency code</param>
        /// <param name="amount">The payment amount in the major currency. Omitting the amount or providing 0 will perform a card verification.</param>
        public PaymentRequest(TSource source, string currency, int? amount)
        {
            Source = source;
            Amount = amount;
            Currency = currency;
        }
        /// <summary>
        /// The source of the payment
        /// </summary>
        public TSource Source { get; }
        /// <summary>
        /// The payment amount in the major currency. Omitting the amount or providing 0 will perform a card verification.
        /// </summary>
        public int? Amount { get; }
        /// <summary>
        /// The three-letter ISO currency code
        /// </summary>
        public string Currency { get; }
        /// <summary>
        /// Must be specified for card payments where the cardholder is not present (recurring or Merchant Offline Telephone Order)
        /// </summary>
        public PaymentType? PaymentType { get; set; }
        /// <summary>
        /// A reference you can later use to identify this payment such as an order number
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// A description of the payment
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Whether to capture the payment (if applicable)
        /// </summary>
        public bool? Capture { get; set; }
        /// <summary>
        /// An ISO 8601 timestamp that determines when the payment should be captured. Providing this field will automatically set capture to true.
        /// </summary>
        public DateTimeOffset? CaptureOn { get; set; }
        /// <summary>
        /// Details of the customer associated with the payment
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// An optional dynamic billing descriptor displayed on the account owner's statement. 
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }
        /// <summary>
        /// The payment shipping details
        /// </summary>
        public Shipping Shipping { get; set; }
        /// <summary>
        /// Whether to process this payment as a 3D-Secure
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public bool? ThreeDS { get; set; }
        /// <summary>
        /// Determines whether to attempt a 3D-Secure payment as non-3DS should the card issuer not be enrolled.
        /// </summary>
        public bool? AttemptN3d { get; set; }
        /// <summary>
        /// For payments that use stored card details such as recurring payments, an existing payment identifier from the recurring series or the Scheme Transaction Id. 
        /// </summary>
        public string PreviousPaymentId { get; set; }
        /// <summary>
        /// Indicates whether risk checks for the requested payment should be skipped
        /// </summary>
        public bool? SkipRiskCheck { get; set; }
        /// <summary>
        /// For redirect payment methods, overrides the default success redirect URL configured on your account
        /// </summary>
        public string SuccessUrl { get; set; }
        /// <summary>
        /// For redirect payment methods, overrides the default failure redirect URL configured on your account
        /// </summary>
        public string FailureUrl { get; set; }
        /// <summary>
        /// The IP address used to make the payment. Required for some risk checks.
        /// </summary>
        public string PaymentIp { get; set; }
        /// <summary>
        /// Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions.
        /// </summary>
        public PaymentRecipient Recipient { get; set; }
        /// <summary>
        /// For OpenPay payments, destinations determine the proportion of the payment amount that should be credited to other OpenPay accounts
        /// </summary>
        public IEnumerable<PaymentDestination> Destinations { get; set; }
        /// <summary>
        /// Set of key/value pairs that you can attach to a payment. It can be useful for storing additional information in a structured format
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}