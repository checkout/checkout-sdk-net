using Checkout.Common;
using System;

namespace Checkout.Payments.Hosted
{

	public sealed class HostedPaymentResponse : Resource, IEquatable<HostedPaymentResponse>
	{
		public string Reference { get; set; }

        public bool Equals(HostedPaymentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Reference == other.Reference;
        }
    }
}