using System;
using Newtonsoft.Json;

using Checkout.Common;
using PaymentSetupRisk = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Risk;
using PaymentSetupThreeds = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Threeds;
using PaymentSetupProcessing = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Processing;
using PaymentSetupBalances = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Balances;
using PaymentSetupSubscription = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Subscription;
using PaymentSetupRetry = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Retry;

namespace Checkout.Payments.Setups
{
    /// <summary>
    /// Payment setup confirmation response
    /// </summary>
    public class PaymentSetupsConfirmResponse : Resource
    {
        /// <summary>
        /// The payment's unique identifier
        /// ^(pay)_(\w{26})$
        /// 30 characters
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique identifier for the action performed against this payment
        /// ^(act)_(\w{26})$
        /// 30 characters
        /// [Required]
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// The payment amount
        /// [Required]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the payment
        /// 3 characters
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Whether or not the authorization or capture was successful
        /// [Required]
        /// </summary>
        public bool? Approved { get; set; }

        /// <summary>
        /// The status of the payment
        /// [Required]
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// The Gateway response code
        /// [Required]
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// The date and time at which the payment was processed
        /// [Required]
        /// </summary>
        public DateTime? ProcessedOn { get; set; }

        /// <summary>
        /// The full amount from the original authorization, if a partial authorization was requested and approved
        /// </summary>
        public long? AmountRequested { get; set; }

        /// <summary>
        /// The acquirer authorization code if the payment was authorized
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// The Gateway response summary
        /// </summary>
        public string ResponseSummary { get; set; }

        /// <summary>
        /// The timestamp (ISO 8601 code) for when the authorization's validity period expires
        /// </summary>
        public string ExpiresOn { get; set; }

        /// <summary>
        /// Provides 3D Secure enrollment status if the payment was downgraded to non-3D Secure
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public PaymentSetupThreeds.Threeds ThreeDSecure { get; set; }

        /// <summary>
        /// Returns the payment's risk assessment results
        /// </summary>
        public PaymentSetupRisk.Risk Risk { get; set; }

        /// <summary>
        /// The source of the payment
        /// </summary>
        public PaymentSetupSource Source { get; set; }

        /// <summary>
        /// The customer associated with the payment, if provided in the request
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// The payment balances
        /// </summary>
        public PaymentSetupBalances.Balances Balances { get; set; }

        /// <summary>
        /// Your reference for the payment
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The details of the subscription
        /// </summary>
        public PaymentSetupSubscription.Subscription Subscription { get; set; }

        /// <summary>
        /// Returns information related to the processing of the payment
        /// </summary>
        public PaymentSetupProcessing.Processing Processing { get; set; }

        /// <summary>
        /// The final Electronic Commerce Indicator (ECI) security level used to authorize the payment. 
        /// Applicable for 3D Secure and network token payments
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// The scheme transaction identifier
        /// </summary>
        public string SchemeId { get; set; }
        
        /// <summary>
        /// The retry information
        /// </summary>
        public PaymentSetupRetry.Retry Retry { get; set; }
    }
}