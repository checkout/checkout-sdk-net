using System;
using Newtonsoft.Json;

using Checkout.Common;
using PaymentSetupRisk = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Risk;
using PaymentSetupThreeds = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Threeds;
using PaymentSetupProcessing = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Processing;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsConfirmResponse : Resource
    {
        /// <summary>
        /// The payment's unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique identifier for the action performed against this payment
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// The payment amount
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the payment
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Whether or not the authorization or capture was successful
        /// </summary>
        public bool? Approved { get; set; }

        /// <summary>
        /// The status of the payment
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The acquirer authorization code if the payment was authorized
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// The Gateway response code
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// The Gateway response summary
        /// </summary>
        public string ResponseSummary { get; set; }

        /// <summary>
        /// Provides 3D Secure enrollment status if the payment was downgraded to non-3D Secure
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public PaymentSetupThreeds.Threeds Threeds { get; set; }

        /// <summary>
        /// Returns the payment's risk assessment results
        /// </summary>
        public PaymentSetupRisk.Risk Risk { get; set; }

        /// <summary>
        /// The source of the payment
        /// </summary>
        public PaymentSetupSource Source { get; set; }

        /// <summary>
        /// The customer associated with the payment
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// The date/time the payment was processed
        /// </summary>
        public DateTime? ProcessedOn { get; set; }

        /// <summary>
        /// Your reference for the payment
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Returns information related to the processing of the payment
        /// </summary>
        public PaymentSetupProcessing.Processing Processing { get; set; }

        /// <summary>
        /// The final Electronic Commerce Indicator (ECI) security level used to authorize the payment
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// The scheme transaction identifier
        /// </summary>
        public string SchemeId { get; set; }
    }
}