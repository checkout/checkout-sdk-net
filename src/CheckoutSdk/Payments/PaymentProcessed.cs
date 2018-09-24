using Newtonsoft.Json;
using System;
using Checkout.Common;

namespace Checkout.Payments
{
    public class PaymentProcessed : Resource
    {
        /// <summary>
        /// Payment unique identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The unique identifier for the action performed against this payment
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// The payment amount
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The three-letter ISO currency code of the payment
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Whether the payment request was approved
        /// </summary>
        public bool Approved { get; set; }
        /// <summary>
        /// The status of the payment
        /// </summary>
        public PaymentStatus? Status { get; set; }
        /// <summary>
        /// The acquirer authorization code if the payment was Authorized
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// Gateway response code
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// The Gateway response summary
        /// </summary>
        public string ResponseSummary { get; set; }
        /// <summary>
        /// Provides 3D-Secure enrollment status if the payment was downgraded to Non-3DS
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDSEnrollment ThreeDS { get; set; }
        /// <summary>
        /// Returns the payments risk assessment results
        /// </summary>
        public Risk Risk { get; set; }
        /// <summary>
        /// The source of the payment
        /// </summary>
        [JsonConverter(typeof(SourceResponseConverter))]
        public IResponsePaymentSource Source { get; set; }
        /// <summary>
        /// The customer to which this payment is linked
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// The date/time the payment was processed
        /// </summary>
        public DateTime ProcessedOn { get; set; }
        /// <summary>
        /// Your reference for the payment
        /// </summary>
        public string Reference { get; set; }
        public Link GetActionsLink() => GetLink("actions");
        public bool CanCapture() => HasLink("capture");
        public Link GetCaptureLink() => GetLink("capture");
        public bool CanVoid() => HasLink("void");
        public Link GetVoidLink() => GetLink("void");
    }
}