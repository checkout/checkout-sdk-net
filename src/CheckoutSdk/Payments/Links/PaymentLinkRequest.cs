using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Links
{
    public sealed class PaymentLinkRequest : IEquatable<PaymentLinkRequest>
	{
		public long? Amount { get; set; }

		public Currency? Currency { get; set; }

		public string Reference { get; set; }

		public string Description { get; set; }

		public int? ExpiresIn { get; set; } 

		public CustomerRequest Customer { get; set; }

		public ShippingDetails Shipping { get; set; }

		public BillingInformation Billing { get; set; }

		public PaymentRecipient Recipient { get; set; }

		public ProcessingSettings Processing { get; set; }

		public IList<Product> Products { get; set; }

		public IDictionary<string, object> Metadata { get; set; }

		public ThreeDsRequest ThreeDs { get; set; }

		public RiskRequest Risk { get; set; }

		public string ReturnUrl { get; set; }

		public string Locale { get; set; }

		public bool? Capture { get; set; }

		public DateTime? CaptureOn { get; set; }

		public bool Equals(PaymentLinkRequest other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Amount.Equals(other.Amount) && Currency.Equals(other.Currency) && Reference.Equals(other.Reference)
				&& Description.Equals(other.Description);
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is PaymentLinkRequest other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Amount, Currency, Reference, Description);
		}

	}

}