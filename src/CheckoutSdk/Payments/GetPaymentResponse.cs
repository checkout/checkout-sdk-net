using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents the details of a payment.
    /// </summary>
    public class GetPaymentResponse : Resource
    {
        /// <summary>
        /// Gets the unique identifier of the payment.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the date/time the payment was requested.
        /// </summary>
        public DateTime RequestedOn { get; set; }
        
        /// <summary>
        /// Gets the source of the payment.
        /// </summary>
        [JsonConverter(typeof(ResponseSourceConverter))]
        public IResponseSource Source { get; set; }
        
        /// <summary>
        /// Gets the original payment amount.
        /// </summary>
        public int? Amount { get; set; }
        
        /// <summary>
        /// Gets the three-letter ISO currency code of the payment.
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Gets the payment type.
        /// </summary>
        public PaymentType PaymentType { get; set; }
        
        /// <summary>
        /// Gets your reference for the payment.
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Gets your description of the payment.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets the status of the payment.
        /// </summary>
        public PaymentStatus Status { get; set; }
        
        /// <summary>
        /// Gets 3D-Secure information relating to the payment.
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDSEnrollment ThreeDS { get; set; }
        
        /// <summary>
        /// Gets the payment's risk response.
        /// </summary>
        public RiskAssessment Risk { get; set; }
        
        /// <summary>
        /// Gest the customer to which this payment is linked.
        /// </summary>
        public Customer Customer { get; set; }
        
        /// <summary>
        /// Gets the billing descriptor displayed on the account owner's statement.
        /// </summary>
        public BillingDescriptor BillingDescriptor { get; set; }
        
        /// <summary>
        /// Gets the payment's shipping details.
        /// </summary>
        public ShippingDetails Shipping { get; set; }
        
        /// <summary>
        /// Gets the customer IP address used to make the payment.
        /// </summary>
        public string PaymentIp { get; set; }
        
        /// <summary>
        /// Gets the payment recipient details (Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions).
        /// </summary>
        public PaymentRecipient Recipient { get; set; }
        
        /// <summary>
        /// Gets the metadata you attached to the original payment request.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// The final Electronic Commerce Indicator security level used to authorize the payment. Applicable for 3D-Secure, digital wallets and network token payments.
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// Determines whether the payment requires a redirect.
        /// </summary>
        /// <returns>True if a redirect is required, otherwise False.</returns>
        public bool RequiresRedirect() => HasLink("redirect");
        
        /// <summary>
        /// Gets the redirect link.
        /// </summary>
        /// <returns>The link if present, otherwise null.</returns>
        public Link GetRedirectLink() => GetLink("redirect");
    }
}