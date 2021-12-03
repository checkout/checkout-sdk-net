using Checkout.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Hosted
{
	public sealed class HostedPaymentRequest : IEquatable<HostedPaymentRequest>
	{

		public long? Amount { get; set; }

		public Currency Currency { get; set; }

		public string Reference { get; set; }

		public string Description { get; set; }

		public CustomerRequest Customer { get; set; }

		public ShippingDetails ShippingDetails { get; set; }

		public BillingInformation Billing { get; set; }

		public PaymentRecipient Recipient { get; set; }

		public ProcessingSettings Processing { get; set; }

		public IList<Product> Products { get; set; }

		public RiskRequest Risk { get; set; }

		[JsonProperty(PropertyName = "success_url")]
		public string SuccessUrl { get; set; }

		[JsonProperty(PropertyName = "cancel_url")]
		public string CancelUrl { get; set; }

		[JsonProperty(PropertyName = "failure_url")]
		public string FailureUrl { get; set; }

		public IDictionary<string, object> Metadata { get; set; }

		public string Locale { get; set; }

		[JsonProperty(PropertyName = "3ds")]
		public ThreeDsRequest ThreeDS { get; set; }

		public bool Capture { get; set; }

		[JsonProperty(PropertyName = "capture_on")]
		public DateTime? CaptureOn { get; set; }

        public bool Equals(HostedPaymentRequest other)
        {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Amount == other.Amount && Currency == other.Currency && Reference == other.Reference &&
				Description == other.Description && Customer.Equals(other.Customer) && ShippingDetails.Equals(other.ShippingDetails) &&
				Billing.Equals(other.Billing) && Recipient.Equals(other.Recipient) && Processing.Equals(other.Processing) && Products.Equals(other.Products) &&
				Risk.Equals(other.Risk) && SuccessUrl.Equals(other.SuccessUrl) && FailureUrl.Equals(other.FailureUrl) && Metadata.Equals(other.Metadata) &&
				Locale.Equals(other.Locale) && ThreeDS.Equals(other.ThreeDS) && Capture == other.Capture && CaptureOn == other.CaptureOn;
		}
    }
}