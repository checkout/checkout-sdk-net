using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.Util;
using Newtonsoft.Json;
using System;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated
{
    /// <summary>
    /// Request a payment or payout Response 201
    /// Payment processed successfully
    /// </summary>
    public class RequestAPaymentOrPayoutResponseCreated : Resource
    {
        /// <summary>
        /// The payment's unique identifier
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique identifier for the action performed against this payment
        /// [Required]
        /// ^(act)_(\w{26})$
        /// 30 characters
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// The payment amount.
        /// [Required]
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the payment
        /// [Required]
        /// 3 characters
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Whether or not the authorization or capture was successful
        /// [Required]
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// The status of the payment
        /// [Required]
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// The Gateway response code
        /// [Required]
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// The date/time the payment was processed
        /// [Required]
        /// <date-time>
        /// </summary>
        public DateTime ProcessedOn { get; set; }

        /// <summary>
        /// The full amount from the original authorization, if a partial authorization was requested and approved.
        /// [Optional]
        /// </summary>
        public int AmountRequested { get; set; }

        /// <summary>
        /// The acquirer authorization code if the payment was authorized
        /// [Optional]
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// The Gateway response summary
        /// [Optional]
        /// </summary>
        public string ResponseSummary { get; set; }

        /// <summary>
        /// The timestamp (ISO 8601 code) for when the authorization's validity period expires
        /// [Optional]
        /// </summary>
        public string ExpiresOn { get; set; }

        /// <summary>
        /// Provides 3D Secure enrollment status if the payment was downgraded to non-3D Secure
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public Threeds.Threeds Threeds { get; set; }

        /// <summary>
        /// Returns the payment's risk assessment results
        /// [Optional]
        /// </summary>
        public Risk.Risk Risk { get; set; }

        /// <summary>
        /// The source of the payment
        /// [Optional]
        /// </summary>
        [JsonConverter(typeof(RequestAPaymentOrPayoutResponseCreatedSourceTypeConverter))]
        public AbstractSource Source { get; set; }

        /// <summary>
        /// The customer associated with the payment, if provided in the request
        /// [Optional]
        /// </summary>
        public Customer.Customer Customer { get; set; }

        /// <summary>
        /// The payment balances
        /// [Optional]
        /// </summary>
        public Balances.Balances Balances { get; set; }

        /// <summary>
        /// Your reference for the payment
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The details of the subscription.
        /// [Optional]
        /// </summary>
        public Subscription.Subscription Subscription { get; set; }

        /// <summary>
        /// Returns information related to the processing of the payment
        /// [Optional]
        /// </summary>
        public Processing.Processing Processing { get; set; }

        /// <summary>
        /// The final Electronic Commerce Indicator (ECI) security level used to authorize the payment. Applicable for
        /// 3D Secure and network token payments
        /// [Optional]
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// The scheme transaction identifier
        /// [Optional]
        /// </summary>
        public string SchemeId { get; set; }

        /// <summary>
        /// Configuration relating to asynchronous retries
        /// [Optional]
        /// </summary>
        public Retry.Retry Retry { get; set; }
    }
}