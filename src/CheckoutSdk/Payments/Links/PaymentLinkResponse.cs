using Checkout.Common;
using Newtonsoft.Json;
using System;

namespace Checkout.Payments.Links
{
	public sealed class PaymentLinkResponse : Resource, IEquatable<PaymentLinkResponse>
	{

		public string Id { get; set; }

		[JsonProperty(PropertyName = "expires_on")]
		public string ExpiresOn { get; set; }

		public string Reference { get; set; }

		public bool Equals(PaymentLinkResponse other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id == other.Id && ExpiresOn == other.ExpiresOn && Reference == other.Reference;
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is PaymentLinkResponse other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, ExpiresOn, Reference);
		}
	}

}