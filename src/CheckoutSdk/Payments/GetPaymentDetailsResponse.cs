using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class GetPaymentDetailsResponse : Resource
    {
        /// <summary>
        /// Payment unique identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The date/time the payment was requested
        /// </summary>
        public DateTime RequestedOn { get; set; }
        /// <summary>
        /// The source of the payment
        /// </summary>
        [JsonConverter(typeof(SourceResponseConverter))]
        public IResponsePaymentSource Source { get; set; }
        /// <summary>
        /// The original payment amount
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// The three-letter ISO currency code of the payment
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Must be specified for card payments where the cardholder is not present (recurring or Merchant Offline Telephone Order)
        /// </summary>
        public PaymentType? PaymentType { get; set; }
        /// <summary>
        /// Your reference for the payment
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// A description of the payment
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The status of the payment
        /// </summary>
        public PaymentStatus? Status { get; set; }
        /// <summary>
        /// Provides information relating to the processing of 3D-Secure payments
        /// </summary>
        [JsonProperty(PropertyName = "3ds")]
        public ThreeDsEnrollment ThreeDs { get; set; }
        /// <summary>
        /// Returns the payments risk assessment results
        /// </summary>
        public Risk Risk { get; set; }
        /// <summary>
        /// The customer to which this payment is linked
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
        /// The IP address used to make the payment
        /// </summary>
        public string PaymentIp { get; set; }
        /// <summary>
        /// Required by VISA and MasterCard for domestic UK transactions processed by Financial Institutions. 
        /// </summary>
        public PaymentRecipient Recipient { get; set; }
        /// <summary>
        /// For OpenPay payments, destinations determine the proportion of the payment amount credited to other OpenPay accounts
        /// </summary>
        public IEnumerable<PaymentDestination> Destinations { get; set; }
        /// <summary>
        /// Set of key/value pairs that you can attach to a payment. It can be useful for storing additional information in a structured format
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        public bool RequiresRedirect() => HasLink("redirect");
        public Link GetRedirectLink() => GetLink("redirect");
    }
}