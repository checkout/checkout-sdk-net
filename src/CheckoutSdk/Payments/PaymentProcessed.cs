using Newtonsoft.Json;
using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Indicates the payment has been successfully processed. 
    /// Check the <see cref="PaymentProcessed.Approved"/> to determine if the payment was approved or declined.
    /// </summary>
    public class PaymentProcessed : Resource
    {
        /// <summary>
        /// Gets the the unique identifier of the payment.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the unique identifier for the action performed against this payment.
        /// </summary>
        public string ActionId { get; set; }
        
        /// <summary>
        /// Gets the payment amount.
        /// </summary>
        public long Amount { get; set; }
        
        /// <summary>
        /// Gets the three-letter ISO currency code of the payment.
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Gets whether the payment request was approved or declined.
        /// </summary>
        public bool Approved { get; set; }
        
        /// <summary>
        /// Gets the status of the payment.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Gets the acquirer authorization code if the payment was Authorized.
        /// </summary>
        public string AuthCode { get; set; }
        
        /// <summary>
        /// Gets the Gateway response code.
        /// </summary>
        public string ResponseCode { get; set; }
        
        /// <summary>
        /// Gets the Gateway response summary.
        /// </summary>
        public string ResponseSummary { get; set; }
        
        /// <summary>
        /// Gets the 3D-Secure enrollment status if the payment was downgraded to Non-3DS.
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDSEnrollment ThreeDS { get; set; }
        
        /// <summary>
        /// Gets the payment's risk response.
        /// </summary>
        public RiskAssessment Risk { get; set; }
        
        /// <summary>
        /// Gets the source of the payment.
        /// </summary>
        [JsonConverter(typeof(ResponseSourceConverter))]
        public IResponseSource Source { get; set; }
        
        /// <summary>
        /// Gets the customer to which this payment is linked.
        /// </summary>
        public CustomerResponse Customer { get; set; }
        
        /// <summary>
        /// Gets the date/time the payment was processed.
        /// </summary>
        public DateTime ProcessedOn { get; set; }
        
        /// <summary>
        /// Gets your reference for the payment.
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Gets the acquirer information related to the processing of the payment
        /// </summary>
        public ProcessingResponse Processing { get; set; }

        /// <summary>
        /// The final Electronic Commerce Indicator security level used to authorize the payment. Applicable for 3D-Secure, digital wallets and network token payments.
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// Gets the payment's actions link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetActionsLink() => GetLink("actions");
        
        /// <summary>
        /// Determines whether the payment can be captured. 
        /// </summary>
        /// <returns>True if the payment can be captured, otherwise False.</returns>
        public bool CanCapture() => HasLink("capture");
        
        /// <summary>
        /// Gets the payment's capture link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetCaptureLink() => GetLink("capture");
        
         /// <summary>
        /// Determines whether the payment can be voided.
        /// </summary>
        /// <returns>True if the payment can be voided, otherwise False.</returns>       
        public bool CanVoid() => HasLink("void");
        
        /// <summary>
        /// Gets the payment's void link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetVoidLink() => GetLink("void");
    }
}